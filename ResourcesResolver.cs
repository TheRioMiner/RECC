using System;
using System.Reflection;
using System.Windows.Forms;

namespace Rio_External_Csgo_Cheat
{
    public class ResourcesResolver
    {
        public ResourcesResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                string assembly = args.Name.Split(',')[0];

                switch (assembly)
                {
                    case "MetroFramework":
                        return Assembly.Load(Properties.Resources.MetroFramework);
                    case "MetroFramework.Fonts":
                        return Assembly.Load(Properties.Resources.MetroFramework_Fonts);

                    default:
                        if (assembly.EndsWith(".resources"))
                            return null;
                        else
                            throw new Exception($"Сборка: \"{assembly}\" не может быть загружена.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Критическая ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }
        }
    }
}
