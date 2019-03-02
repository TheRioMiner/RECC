using System;
using System.Threading;
using System.Windows.Forms;

using Rio_External_Csgo_Cheat.CSGO;
using Rio_External_Csgo_Cheat.Properties;

namespace Rio_External_Csgo_Cheat
{
    static class Program
    {
        public static bool IsClosing = false;
        public static string MyCRC = "invalid";
        public static ResourcesResolver ResourcesResolver = new ResourcesResolver();



        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Навсякий случай еще раз инициализируем класс загрузчик внутренних ресурсов
            ResourcesResolver = new ResourcesResolver();

            //Включаем визуальные стили
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Проверяем работает ли драйвер?
            Retry:
            if (!KernelBlackMagic.CheckDriverIsWorking())
            {
                Thread.Sleep(100);
                var dialResult = MessageBox.Show("Драйвер KernelBlackMagic не работает! Запустите его!", "Ошибка!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (dialResult == DialogResult.Retry)
                    goto Retry;
                else
                    return;
            }


            //Авторизируемся
            var af = new Auth_Form();
            af.ShowDialog();


            MyCRC = af.CrcStr;

            //Проверяем авторизацию
            if (!af.auth_metroButton.Enabled && !af.FakeAccess && af.AccessGranted)
            {
                if (!af.auth_metroButton.Enabled && !af.FakeAccess && af.AccessGranted && af.Access.ToLower() == "yep!")
                {
                    //Сохраняем ключик который мы вводили
                    Thread.Sleep(250);
                    Settings.Default.KeySaved = af.key_metroTextBox.Text;
                    Settings.Default.Save();
                    Thread.Sleep(250);



                    //Чекаем что дрова работают
                    while (!KernelBlackMagic.CheckDriverIsWorking())
                    {
                        Thread.Sleep(100);
                        var dialResult = MessageBox.Show("Драйвер KernelBlackMagic не работает! Запустите его!", "Ошибка!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (dialResult == DialogResult.Retry)
                            continue;
                        else
                            return;
                    }

                    //Инициализируем чёрную магию на игру
                    while (!KernelBlackMagic.InitializateMagic("csgo.exe"))
                    {
                        var dialResult = MessageBox.Show("CSGO не запущена!", "Ошибка!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (dialResult == DialogResult.Retry)
                            continue;
                        else
                            return;
                    }



                    //Грузим оффсеты!
                    if (af.Access == "YEP!")
                    {
                        //Если неудача, то ливаем!
                        if (!Offsets.LoadOffsets())
                            return;
                    }


                    //Инициализируем форму
                    try
                    {
                        if (af.Access.ToLower() == "yep!" && !af.FakeAccess && af.AccessGranted && af.Access.ToUpper().Trim() == "yEp!".ToUpper())
                        {
                            //Запускаем шарманку
                            Application.Run(new Main_Form());


                            //Форму закрыли, сообщаем потокам что они могут завершится
                            IsClosing = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Неизвестная ошибка: [{ex.Message}]", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
