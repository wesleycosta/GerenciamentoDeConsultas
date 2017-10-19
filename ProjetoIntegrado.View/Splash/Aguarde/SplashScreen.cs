namespace ProjetoIntegrado.View
{
    public static class SplashScreen
    {
        private static bool visivel;
        private static SplashScreenWin frmSplashScreen;

        public static void Mostrar()
        {
            Fechar();

            frmSplashScreen = new SplashScreenWin();
            frmSplashScreen.Show();
            visivel = true;
        }

        public static void Fechar()
        {
            if (visivel)
            {
                frmSplashScreen.Close();
                visivel = false;
            }
        }
    }
}
