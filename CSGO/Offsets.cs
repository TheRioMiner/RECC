using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Rio_External_Csgo_Cheat.CSGO
{
    public class Offsets
    {
        public static bool LoadOffsets()
        {
            if (File.Exists("Offsets.toml"))
            {
                string toml = File.ReadAllText("Offsets.toml");

                if (!ParseToml(toml))
                {
                    MessageBox.Show("Ошибка парсинга оффсетов с файла, пожалуйста убедитесь в целостности файла 'Offsets.toml'!");
                    return false;
                }
                return true;
            }
            else
            {
                string toml = "";
                try
                {
                    toml = Tools.DownloadHtml("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.toml"); //Обычные
                }
                catch
                {
                    MessageBox.Show("Невозможно скачать файл с оффсетами из интернета! Возможно нету интернета?");
                    return false;
                }

                if (!ParseToml(toml))
                {
                    MessageBox.Show("Ошибка парсинга скачанных оффсетов! Пожалуйста попробуйте еще раз или обновите чит!");
                    return false;
                }
                return true;
            }
        }


        private static bool ParseToml(string toml)
        {
            _offsets.Clear(); //Очищаем словарь оффсетов

            //Построчно парсим и добавляем в словарь
            string[] lines = toml.Split('\n');
            for (uint i = 0; i < lines.Length; i++)
            {
                string currLine = lines[i].Trim().ToLower();

                //Игнорим секции и пустые линии
                if (currLine.StartsWith("[") || currLine.EndsWith("]") || (currLine == ""))
                    continue;

                string[] splited = currLine.Split('=');
                if (splited.Length == 2) //Если разделили нормально
                {
                    uint offset = 0;
                    if (uint.TryParse(splited[1].Trim(), out offset))
                        _offsets.Add(splited[0].Trim().ToLower(), offset);
                    else
                        return false; //Да что за херня? Файл поврежден!
                }
                else
                    return false; //Файл поврежден!
            }

            //Успешно пропарсили!
            return true;
        }



        private static Dictionary<string, uint> _offsets = new Dictionary<string, uint>();


        public uint this[string offsetName]
        {
            get
            {
                if (_offsets.ContainsKey(offsetName.ToLower()))
                    return _offsets[offsetName.ToLower()];
                else
                {
                    MessageBox.Show($"Критическая ошибка! Оффсет: {offsetName} - не найден! Обратитесь к разработчику чита!", "Критическая ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception(offsetName);
                }
            }
        }
    }
}
