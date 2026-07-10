using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SakilaApp.Areas.Identity.Pages.Account.Manage;

[Authorize]
public class EnableAuthenticatorModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly UrlEncoder _urlEncoder;

    public EnableAuthenticatorModel(
        UserManager<IdentityUser> userManager,
        UrlEncoder urlEncoder)
    {
        _userManager = userManager;
        _urlEncoder = urlEncoder;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    public string SharedKey { get; set; } = string.Empty;

    public string AuthenticatorUri { get; set; } = string.Empty;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 6)]
        [Display(Name = "Código de verificación")]
        public string Code { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return NotFound($"No se pudo cargar el usuario con el ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadSharedKeyAndQrCodeUriAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return NotFound($"No se pudo cargar el usuario con el ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadSharedKeyAndQrCodeUriAsync(user);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var verificationCode = Input.Code.Replace(" ", string.Empty, StringComparison.Ordinal)
                                         .Replace("-", string.Empty, StringComparison.Ordinal);

        var is2FaTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            TokenOptions.DefaultAuthenticatorProvider,
            verificationCode);

        if (!is2FaTokenValid)
        {
            ModelState.AddModelError(string.Empty, "El código de verificación no es válido.");
            return Page();
        }

        await _userManager.SetTwoFactorEnabledAsync(user, true);
        StatusMessage = "Tu app de autenticación ha sido verificada.";
        return RedirectToPage("./GenerateRecoveryCodes");
    }

    private async Task LoadSharedKeyAndQrCodeUriAsync(IdentityUser user)
    {
        var key = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(key))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            key = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        SharedKey = FormatKey(key!);
        AuthenticatorUri = GenerateQrCodeUri(user.Email ?? user.UserName ?? string.Empty, key!);
    }

    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6",
            _urlEncoder.Encode("SakilaApp"),
            _urlEncoder.Encode(email),
            unformattedKey);
    }

    private static string FormatKey(string unformattedKey)
    {
        var result = string.Empty;
        for (var i = 0; i < unformattedKey.Length; i += 4)
        {
            var length = Math.Min(4, unformattedKey.Length - i);
            result += unformattedKey.Substring(i, length) + " ";
        }

        return result.Trim().ToLowerInvariant();
    }
}
