using UnityEditor;

namespace Glitch9.Auth
{
    /// <summary>
    /// Not UI, called from other Editor UIs to draw the sign in panel.
    /// </summary>
    internal static class EditorSignIn
    {
        internal static class PrefsKeys
        {
            internal const string Email = "Glitch9.Editor.Auth.Email";
            internal const string Password = "Glitch9.Editor.Auth.Password";
        }

        /*
            Basic Layouot:
            == Glitch 9 Inc. Logo or Text ==
            Email: [InputField]
            Password: [InputField] (Password)
            [Sign In Button]
            [Forgot Password Button]
            [Create Account Button]

            == Social Sign In Buttons == // supports 2 at this time (Google, GitHub)
            [Sign In with Google]
            [Sign In with GitHub]

            Create above separatly, so that we can adjust the layout later if needed.
        */

        internal static void DrawLogo()
        {

        }

        internal static void DrawEmailField()
        {

        }

        internal static void DrawPasswordField()
        {

        }

        internal static void DrawSignInButton()
        {

        }

        internal static void DrawForgotPasswordButton()
        {

        }

        internal static void DrawCreateAccountButton()
        {

        }

        internal static void DrawSignInWithGoogleButton()
        {

        }

        internal static void DrawSignInWithGitHubButton()
        {

        }
    }
}