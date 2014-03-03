using System.Windows.Controls;

namespace PAL_PlayAndLearn
{
    public static class Switcher
    {
        public static ContentControl MainProgramSwitcher;

        public static ContentControl StartUpProgramSwitcher;

        public static ContentControl ProfileContentSwitcher;
        public static void Switch(ContentControl pageSwitcher,UserControl newPage)
        {
            pageSwitcher.Content = newPage;
        }
    }
}
