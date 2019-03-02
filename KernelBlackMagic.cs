using System;
using System.Text;
using System.Management;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Rio_External_Csgo_Cheat
{
    /// <summary>
    /// KernelBlackmagic Lib v2.2.1
    /// For Driver v2.2
    /// </summary>
    public unsafe class KernelBlackMagic
    {
        //Защищенная лоу-левельная зона адрессов
        private const uint ProtectedLowAddressZone = ((1024 * 1024) * 1); //1MB


        //Список режимов вывода отладочной информации
        public enum DebugPrintParams : byte
        {
            NONE = 0,
            DEBUG_PRINT_ALL,
            DEBUG_PRINT_ONLY_ERRORS,
            DEFAULT = DEBUG_PRINT_ONLY_ERRORS
        };



        /// <summary>
        /// Инициализировать магию!
        /// </summary>
        /// <param name="TargetPID">PID жертвы</param>
        public static bool InitializateMagic(ushort TargetPID, DebugPrintParams DebugMode = DebugPrintParams.DEFAULT)
        {
            return InternalMagic.InitializateMagic(TargetPID, DebugMode);
        }

        /// <summary>
        /// Инициализировать магию по имени процесса!
        /// </summary>
        /// <param name="ProcessName">Имя процесса жертвы</param>
        public static bool InitializateMagic(string ProcessName, DebugPrintParams DebugMode = DebugPrintParams.DEFAULT)
        {
            return InternalMagic.InitializateMagic(ProcessName, DebugMode);
        }



        #region <- ВСЯКОЕ ->

        /// <summary>
        /// Проверить, работает ли драйвер?
        /// </summary>
        public static bool CheckDriverIsWorking()
        {
            return InternalMagic.DriverIsWorking();
        }


        /// <summary>
        /// Получить базовый адрес процесса жертвы
        /// </summary>
        public static UInt64 GetBaseAddress()
        {
            return InternalMagic.GetBaseAddress();
        }


        /// <summary>
        /// Получить PID по первому вхождению имени процесса
        /// </summary>
        /// <param name="ProcessName">Имя процесса (чуствительно к регистру!)</param>
        public static UInt16 GetPidByProcessName(string ProcessName)
        {
            foreach (ManagementObject mo in new ManagementClass("Win32_Process").GetInstances())
            {
                string processName = mo["Name"] as string;
                if (processName == ProcessName)
                {
                    ushort pidValue = 0;
                    string pid = $"{mo["ProcessId"]}";
                    if (ushort.TryParse(pid, out pidValue))
                        return pidValue;
                }
            }

            //Не нашли
            return 0;
        }


        /// <summary>
        /// Проверка что процесс 64 битный
        /// </summary>
        /// <param name="PID">PID процесса</param>
        /// <param name="Is64Bit">Результат</param>
        public static bool IsProcess64Bit(UInt16 PID, out bool Is64Bit)
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                Is64Bit = false;
                return true;
            }
            else
                return InternalMagic.ProcessIsWOW64(PID, out Is64Bit);
        }


        /// <summary>
        /// Установить текущий режим вывода отладочной информации
        /// </summary>
        public static bool SetDebugMode(DebugPrintParams DebugMode, bool PrintThisInDebug)
        {
            return InternalMagic.SetDebugMode(DebugMode, PrintThisInDebug);
        }

        #endregion




        #region <- ЧТЕНИЕ ->

        /// <summary>
        /// Прочитать в уже инициализированный массив байт
        /// </summary>
        /// <param name="Address">Адрес чтения</param>
        /// <param name="Bytes">Инициализированный массив байт</param>
        public static bool ReadBytes(ulong Address, ref byte[] Bytes)
        {
            if ((Address > ProtectedLowAddressZone) || (Bytes != null))
            {
                fixed (byte* ptr = Bytes) //Фиксируем массив, и после драйвер все пихнет в него сам
                    return InternalMagic.ReadWriteBytes(Address, ptr, (uint)Bytes.Length, true);
            }
            return false;
        }


        /// <summary>
        /// Прочитать число напрямую в указанный адрес
        /// Самый молниеносный способ!
        /// Занимает в среднем 1,151960 тика для uint
        /// </summary>
        /// <param name="Address">Адрес чтения</param>
        /// <param name="ValueAddress">Адрес числа куда будет записыватся число</param>
        /// <param name="ValueSize">Размер числа</param>
        public static bool Read(ulong Address, void* ValueAddress, uint ValueSize)
        {
            if (Address > ProtectedLowAddressZone)
                return InternalMagic.ReadWriteBytes(Address, ValueAddress, ValueSize, true);
            else
                return false;
        }

        /// <summary>
        /// Прочитать число
        /// В среднем занимает 1,220915 тика для uint
        /// </summary>
        /// <typeparam name="T">Число</typeparam>
        /// <param name="Address">Адрес чтения</param>
        /// <param name="Value">Число в которое будет помещен результат</param>
        public static bool Read<T>(ulong Address, ref T Value)
        {
            if (Address > ProtectedLowAddressZone)
            {
                TypedReference tRef = __makeref(Value); //Грязные хаками получаем адрес 'T Value'
                return InternalMagic.ReadWriteBytes(Address, (void*)*(UInt64*)(&tRef), (uint)Marshal.SizeOf(typeof(T)), true);
            }
            return false;
        }

        /// <summary>
        /// Прочитать число
        /// В среднем занимает 1,220915 тика для uint
        /// </summary>
        /// <typeparam name="T">Число</typeparam>
        /// <param name="Address">Адрес чтения</param>
        public static T Read<T>(ulong Address)
        {
            T result = default(T);
            Read(Address, ref result);
            return result;
        }


        /// <summary>
        /// Пройтись по цепочке указателей и после прочитать число
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Address">Первый адрес который будет взят за основу</param>
        /// <param name="Offsets">Оффсеты</param>
        public static T ReadOffset64<T>(params ulong[] Offsets)
        {
            ulong num = 0u;
            for (int i = 0; i < Offsets.Length; i++)
            {
                if (i == (Offsets.Length - 1))
                    return Read<T>(num + Offsets[i]);
                else
                {
                    num = Read<ulong>(num + Offsets[i]);
                    if (num == 0) //После чтения проверяем, не умерший это указатель?
                        break;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Пройтись по цепочке указателей и после прочитать число
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Address">Первый адрес который будет взят за основу</param>
        /// <param name="Offsets">Оффсеты</param>
        public static T ReadOffset32<T>(params uint[] Offsets)
        {
            uint num = 0u;
            for (int i = 0; i < Offsets.Length; i++)
            {
                if (i == (Offsets.Length - 1))
                    return Read<T>(num + Offsets[i]);
                else
                {
                    num = Read<uint>(num + Offsets[i]);
                    if (num == 0) //После чтения проверяем, не умерший это указатель?
                        break;
                }
            }

            return default(T);
        }


        //Строки для кэширования
        private static Dictionary<ulong, string> _cachedStrings = new Dictionary<ulong, string>();

        //Прочитать строку по нужному адрессу и в нужной кодировке с ограничением по длине
        private static string ReadStringInternal(Encoding Encoding, ulong Address, ushort MaxLenght)
        {
            byte[] Array = new byte[MaxLenght];
            fixed (byte* ptr = Array)
            {
                if (InternalMagic.ReadWriteBytes(Address, ptr, (uint)Array.Length, true))
                {
                    //Сквозь массив ищем null терминатор строки
                    for (int i = 0; i < MaxLenght; i++)
                    {
                        if (*(ptr + i) == '\0') //Ищем null терминатор
                            return Encoding.GetString(Array, 0, i);
                    }

                    //Окей, мы не нашли конец строке, вернем все!
                    return Encoding.GetString(Array);
                }
                return ""; //Ошибка!
            }
        }


        /// <summary>
        /// Прочитать строку в указаной кодировке
        /// </summary>
        /// <param name="Encoding">Кодировка из которой будет преобразована строка</param>
        /// <param name="Address">Адрес строки</param>
        /// <param name="MaxLenght">Максимальная длина чтения в байтах</param>
        public static string ReadString(Encoding Encoding, ulong Address, ushort MaxLenght, bool IsCacheString = false)
        {
            if (Address > ProtectedLowAddressZone)
            {
                if (IsCacheString)
                {
                    string str = null;
                    if (!_cachedStrings.TryGetValue(Address, out str))
                    {
                        str = ReadStringInternal(Encoding, Address, MaxLenght);
                        _cachedStrings.Add(Address, str);
                        return str;
                    }
                    else
                        return str;
                }
                else
                    return ReadStringInternal(Encoding, Address, MaxLenght);
            }
            return ""; //Адресс находится в защищеной зоне или ошибка чтения!
        }

        /// <summary>
        /// Прочитать строку в UTF8 кодировке
        /// </summary>
        /// <param name="Address">Адрес строки</param>
        /// <param name="MaxLenght">Максимальная длина чтения в байтах</param>
        /// <returns></returns>
        public static string ReadString(ulong Address, ushort MaxLenght, bool IsCacheString = false)
        {
            return ReadString(Encoding.UTF8, Address, MaxLenght, IsCacheString);
        }

        /// <summary>
        /// Удалить все кэшированные строки
        /// </summary>
        public static void ClearStringCache()
        {
            _cachedStrings.Clear();
        }

        #endregion




        #region <- ЗАПИСЬ ->

        /// <summary>
        /// Записать массив байт
        /// </summary>
        /// <param name="Address">Адрес записи</param>
        /// <param name="Bytes">Массив байт который буем записывать</param>
        public static bool WriteBytes(ulong Address, byte[] Bytes)
        {
            if ((Address > ProtectedLowAddressZone) || (Bytes != null))
            {
                fixed (byte* ptr = Bytes) //Фиксируем массив, и драйвер все сам прочитает
                    return InternalMagic.ReadWriteBytes(Address, ptr, (uint)Bytes.Length, false);
            }
            return false;
        }


        /// <summary>
        /// Записать число в память
        /// Самый молниеносный способ!
        /// В среднем занимает 1,079411 тика для uint
        /// </summary>
        /// <param name="Address">Адрес записи</param>
        /// <param name="ValueAddress">Адрес записываемого числа</param>
        /// <param name="ValueSize">Размер записываемого числа</param>
        public static bool Write(ulong Address, void* ValueAddress, uint ValueSize)
        {
            if (Address > ProtectedLowAddressZone)
                return InternalMagic.ReadWriteBytes(Address, ValueAddress, ValueSize, false);
            else
                return false;
        }

        /// <summary>
        /// Записать число в память
        /// В среднем занимает 1,250326 тика для uint
        /// </summary>
        /// <typeparam name="T">Число</typeparam>
        /// <param name="Address">Адрес записи</param>
        /// <param name="Value">Записываемое число</param>
        public static bool Write<T>(ulong Address, T Value)
        {
            if (Address > ProtectedLowAddressZone)
            {
                TypedReference tRef = __makeref(Value); //Грязные хаками получаем адрес 'T Value'
                return InternalMagic.ReadWriteBytes(Address, (void*)*(UInt64*)(&tRef), (uint)Marshal.SizeOf(typeof(T)), false);
            }
            return false;
        }



        /// <summary>
        /// Записать текст в память
        /// </summary>
        /// <param name="Address">Адрес записи</param>
        /// <param name="Text">Текст который будет записан в память</param>
        public static bool WriteString(ulong Address, string Text)
        {
            if (Address > ProtectedLowAddressZone)
            {
                fixed (char* ptr = Text) //Фиксируем текст и записываем куда надо
                    return InternalMagic.ReadWriteBytes(Address, (void*)(ptr), (uint)Text.Length, false);
            }
            return false;
        }

        #endregion



        /// <summary>
        /// Вспомогательный класс, для получения модулей у процесса
        /// </summary>
        public class Modules
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool CloseHandle(IntPtr hSnapshot);

            [DllImport("kernel32.dll")]
            private static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct MODULEENTRY32
            {
                internal uint dwSize;
                internal uint th32ModuleID;
                internal uint th32ProcessID;
                internal uint GlblcntUsage;
                internal uint ProccntUsage;
                internal IntPtr modBaseAddr;
                internal uint modBaseSize;
                internal IntPtr hModule;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                internal string szModule;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                internal string szExePath;

                public bool IsValid => (hModule != IntPtr.Zero) && (modBaseAddr != IntPtr.Zero) && (modBaseSize != 0);
            };



            public static ulong FindAddress(string ModuleName)
            {
                return GetBaseAddress(InternalMagic.TargetPID, ModuleName);
            }

            public static ulong GetBaseAddress(ushort ProcessID, string ModuleName)
            {
                var module = FindModule(ProcessID, ModuleName);
                if (module.IsValid)
                    return (ulong)module.modBaseAddr;
                else
                    return 0;
            }


            public static uint GetModuleSize(string ModuleName)
            {
                return GetModuleSize(InternalMagic.TargetPID, ModuleName);
            }

            public static uint GetModuleSize(ushort ProcessID, string ModuleName)
            {
                var module = FindModule(ProcessID, ModuleName);
                if (module.IsValid)
                    return (uint)module.modBaseSize;
                else
                    return 0;
            }


            public static MODULEENTRY32 FindModule(ushort ProcessID, string ModuleName)
            {
                IntPtr hSnapshot = CreateToolhelp32Snapshot(0x8 | 0x10, ProcessID);

                if (hSnapshot == new IntPtr(-1))
                    return default(MODULEENTRY32); //Snapshot object create failed!

                MODULEENTRY32 mEntry = new MODULEENTRY32();
                mEntry.dwSize = (uint)Marshal.SizeOf(mEntry);

                do
                {
                    if (mEntry.szModule == ModuleName)
                    {
                        CloseHandle(hSnapshot);
                        return mEntry;
                    }
                }
                while (Module32Next(hSnapshot, ref mEntry));

                //Failed to find the module
                CloseHandle(hSnapshot);
                return default(MODULEENTRY32);
            }
        }



        /// <summary>
        /// Вспомогательный класс для поиска паттернов в памяти
        /// </summary>
        public static class Patterns
        {
            public class CachedPattern
            {
                public byte[] Pattern = null;
                public List<int> IgnoredIndexes = new List<int>();

                public CachedPattern(string pattern)
                {
                    int i = 0;
                    var tempByteArray = new List<byte>();
                    foreach (Match match in Regex.Matches(pattern, @"([0-9a-fA-F]{2})|(\?\?|\?)"))
                    {
                        if (match.Groups[1].Success) //hex
                        {
                            i++;
                            tempByteArray.Add(Convert.ToByte(match.Groups[1].Value, 16));
                        }
                        else if (match.Groups[2].Success) //?
                        {
                            tempByteArray.Add(0);
                            IgnoredIndexes.Add(i++);
                        }
                    }

                    //Getting result
                    Pattern = tempByteArray.ToArray();
                }
            }


            /// <summary>
            /// Найти определенный паттерн в dll
            /// </summary>
            /// <param name="moduleName"></param>
            /// <param name="pattern"></param>
            /// <param name="chunkLenght"></param>
            /// <returns></returns>
            public static ulong Find(string moduleName, string pattern, uint chunkLenght = 1024 * 1024 * 16)
            {
                var patternCached = new CachedPattern(pattern);
                var module = Modules.FindModule(InternalMagic.TargetPID, moduleName);
                if (module.IsValid)
                {
                    byte[] buffer = new byte[chunkLenght];
                    ulong startAddress = (ulong)module.modBaseAddr;
                    ulong endAddress = (ulong)module.modBaseAddr + module.modBaseSize;
                    for (ulong iBuff = startAddress; iBuff < endAddress; iBuff += chunkLenght)
                    {
                        //Buffer overflowing at end address?
                        if ((iBuff + (ulong)(buffer.Length)) > endAddress)
                        {
                            uint diff = (uint)(iBuff + (ulong)(buffer.Length) - endAddress);
                            buffer = new byte[buffer.Length - diff]; //Reinit them!
                        }

                        if (ReadBytes(iBuff, ref buffer))
                        {
                            uint index;
                            if (FindInChunk(buffer, patternCached, out index))
                                return iBuff + index;
                        }
                    }
                }
                return 0;
            }

            //Найти в чанке
            private static bool FindInChunk(byte[] memoryChunk, CachedPattern patternCached, out uint index)
            {
                int insideIndex = 0;
                for (uint i = 0; i < memoryChunk.Length; i++)
                {
                    //Это игнорируемый индекс?
                    if (patternCached.IgnoredIndexes.Contains(insideIndex))
                        insideIndex++;
                    else if (patternCached.Pattern[insideIndex] == memoryChunk[i]) //Иначе совпадает ли паттерн?
                        insideIndex++;
                    else
                        insideIndex = 0;

                    //Мы прошли весь путь?
                    if (insideIndex == patternCached.Pattern.Length)
                    {
                        index = (i - (uint)(insideIndex - 1));
                        return true;
                    }
                }

                //Not found
                index = 0;
                return false;
            }
        }



        /// <summary>
        /// Внутренная, самая главная магия
        /// </summary>
        private unsafe class InternalMagic
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
            [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
            private static extern bool DeviceIoControl(SafeFileHandle hDevice, UInt32 IoControlCode, UIntPtr InBuffer, uint InBufferSize, UIntPtr OutBuffer, uint OutBufferSize, out uint BytesReturned, IntPtr Overlapped);

            //Хендл драйвера
            public static SafeFileHandle _hDriver;

            //Текущий пид процесса
            public static UInt16 TargetPID;

            //Наш пид процесса
            public static UInt16 ThisPID => (UInt16)(Process.GetCurrentProcess().Id);


            /// <summary>
            /// Do device io operations
            /// </summary>
            private static bool DeviceIoControl(uint IOCTL_Code, UIntPtr InObjPtr, int InObjSize)
            {
                uint Received = 0;
                return DeviceIoControl(_hDriver, IOCTL_Code, InObjPtr, (uint)InObjSize, UIntPtr.Zero, 0, out Received, IntPtr.Zero);
            }



            /* Установить процессы с которыми мы сейчас работаем */
            private const uint IOCTL_INITIALIZATE_PROCESSES = (0x22 << 16) | (0x00AA << 2) | 0 | (0 << 14);

            /* Получить базовый адрес */
            private const uint IOCTL_GET_BASEADDRESS = (0x22 << 16) | (0x00AB << 2) | 0 | (0 << 14);

            /* Найти пид по имени процесса */
            private const uint IOCTL_FIND_PID_BY_PROCESS_NAME = (0x22 << 16) | (0x00AC << 2) | 0 | (0 << 14);

            /* Проверка что процесс 64 битный по PID'у */
            private const uint IOCTL_PROCESS_IS_WOW_64_BY_PID = (0x22 << 16) | (0x00AD << 2) | 0 | (0 << 14);


            /* Запрос на чтения/запись любого кол. байт (массивы) */
            private const uint IOCTL_WRITE_BYTES = (0x22 << 16) | (0x00DD << 2) | 0 | (0 << 14);
            private const uint IOCTL_READ_BYTES = (0x22 << 16) | (0x00DE << 2) | 0 | (0 << 14);


            /* Установить нужный режим отладки */
            private const uint IOCTL_SET_DEBUG_MODE = (0x22 << 16) | (0x00FF << 2) | 0 | (0 << 14);



            private struct INITIALIZATE_PROCESSES_REQUEST
            {
                public UInt16 SourcePID;
                public UInt16 HostPID;
            }

            private struct GET_BASEADDRESS_REQUEST
            {
                public UInt64 ResultValueAddress;
            }

            private struct FIND_PID_BY_PROCESS_NAME_REQUEST
            {
                public UInt16 MaxPidSearch;
                public UInt16 HostPID;
                public UInt64 ResultPidAddress;
                public fixed byte ProcessName[255];
            }

            private struct PROCESS_IS_WOW_64_BY_PID_REQUEST
            {
                public UInt16 CheckPID;
                public UInt16 HostPID;
                public UInt64 ResultBooleanAddress;
            }


            private struct RW_BYTES_REQUEST
            {
                public UInt64 SourceAddress;
                public UInt64 HostAddress;
                public UInt32 Size;
            };


            private struct SET_DEBUG_MODE_REQUEST
            {
                public byte DebugMode;
                public bool PrintThisInDebug;
            };



            /// <summary>
            /// Просто проверка работает ли драйвер?
            /// </summary>
            public static bool DriverIsWorking()
            {
                var _hDriver = CreateFile("\\\\.\\kernelblackmagic", 0x80000000 | 0x40000000, 0, IntPtr.Zero, 0x3, 0, IntPtr.Zero);
                bool result = !(_hDriver.IsInvalid || _hDriver.IsClosed);

                _hDriver.Dispose();

                return result;
            }



            /// <summary>
            /// Инициализировать соединение с драйвером
            /// </summary>
            /// <returns></returns>
            public static bool InitializateDriver()
            {
                if (_hDriver != null && !_hDriver.IsClosed)
                    _hDriver.Dispose();

                //GENERIC_READ = 0x80000000, GENERIC_WRITE = 0x40000000, OPEN_EXISTING = 0x3;
                _hDriver = CreateFile("\\\\.\\kernelblackmagic", 0x80000000 | 0x40000000, 0, IntPtr.Zero, 0x3, 0, IntPtr.Zero);

                return (!(_hDriver.IsInvalid || _hDriver.IsClosed));
            }




            /// <summary>
            /// Инициализировать магию!
            /// </summary>
            /// <param name="TargetId">PID жертвы</param>
            public static bool InitializateMagic(ushort TargetPid, DebugPrintParams DebugMode = DebugPrintParams.DEFAULT)
            {
                TargetPID = TargetPid;

                if (InitializateDriver())
                {
                    SetDebugMode(DebugMode, false);
                    return InitializateDriverProcesses(TargetPid);
                }
                else
                    return false;
            }


            /// <summary>
            /// Инициализировать магию!
            /// </summary>
            /// <param name="TargetId">PID жертвы</param>
            public static bool InitializateMagic(string ProcessName, DebugPrintParams DebugMode = DebugPrintParams.DEFAULT)
            {
                if (InitializateDriver())
                {
                    SetDebugMode(DebugMode, false);
                    TargetPID = KernelBlackMagic.GetPidByProcessName(ProcessName);
                    //TargetPID = (UInt16)GetPidByProcessName(ProcessName);
                    if (TargetPID != 0)
                        return InitializateDriverProcesses(TargetPID);
                }
                return false;
            }



            /// <summary>
            /// Инициализировать в драйвере процессы хоста и жертвы
            /// </summary>
            /// <param name="TargetPid">PID жертвы</param>
            public static bool InitializateDriverProcesses(ushort TargetPid)
            {
                //Делаем запрос на инициализацию процессов
                var Request = new INITIALIZATE_PROCESSES_REQUEST();
                Request.SourcePID = TargetPid;
                Request.HostPID = ThisPID;
                return DeviceIoControl(IOCTL_INITIALIZATE_PROCESSES, (UIntPtr)(&Request), sizeof(INITIALIZATE_PROCESSES_REQUEST));
            }


            /// <summary>
            /// Получить базовый адрес жертвы
            /// </summary>
            public static UInt64 GetBaseAddress()
            {
                UInt64 BaseAddress = 0;
                {
                    var Request = new GET_BASEADDRESS_REQUEST();
                    Request.ResultValueAddress = (UInt64)(&BaseAddress);
                    bool result = DeviceIoControl(IOCTL_GET_BASEADDRESS, (UIntPtr)(&Request), sizeof(GET_BASEADDRESS_REQUEST));
                }
                return BaseAddress; //Возвращяем результат
            }


            /// <summary>
            /// Найти pid по имени процесса
            /// </summary>
            public static UInt16 GetPidByProcessName(string ProcessName, UInt16 MaxPidSearch = 9999)
            {
                UInt16 ResultPid = 0;
                {
                    var Request = new FIND_PID_BY_PROCESS_NAME_REQUEST();
                    Request.MaxPidSearch = MaxPidSearch;
                    Request.HostPID = ThisPID;
                    Request.ResultPidAddress = (UInt64)(&ResultPid);
                    byte[] utf8 = Encoding.UTF8.GetBytes(ProcessName.ToCharArray(), 0, Math.Min(ProcessName.Length, 255));
                    for (int i = 0; i < Math.Min(255, ProcessName.Length); i++) { Request.ProcessName[i] = utf8[i]; }
                    bool result = DeviceIoControl(IOCTL_FIND_PID_BY_PROCESS_NAME, (UIntPtr)(&Request), sizeof(FIND_PID_BY_PROCESS_NAME_REQUEST));
                }
                return ResultPid; //Возвращяем результат
            }


            /// <summary>
            /// Проверка что процесс 64 битный
            /// </summary>
            /// <param name="PID">PID процесса</param>
            /// <param name="IsWOW64">Результат</param>
            public static bool ProcessIsWOW64(UInt16 PID, out bool IsWOW64)
            {
                Boolean ResultIsWOW64;
                var Request = new PROCESS_IS_WOW_64_BY_PID_REQUEST();
                {
                    Request.CheckPID = PID;
                    Request.HostPID = ThisPID;
                    Request.ResultBooleanAddress = (UInt64)(&ResultIsWOW64);
                    bool result = DeviceIoControl(IOCTL_PROCESS_IS_WOW_64_BY_PID, (UIntPtr)(&Request), sizeof(FIND_PID_BY_PROCESS_NAME_REQUEST));
                    IsWOW64 = ResultIsWOW64;
                    return result;
                }
            }


            /// <summary>
            /// Установить текущий режим вывода отладочной информации
            /// </summary>
            public static bool SetDebugMode(DebugPrintParams DebugMode, bool PrintThisInDebug)
            {
                var Request = new SET_DEBUG_MODE_REQUEST();
                Request.DebugMode = (byte)DebugMode;
                Request.PrintThisInDebug = PrintThisInDebug;
                return DeviceIoControl(IOCTL_SET_DEBUG_MODE, (UIntPtr)(&Request), sizeof(SET_DEBUG_MODE_REQUEST));
            }



            /// <summary>
            /// Запись/чтение необходимо кол. байт
            /// Самая главная функция в KernelBlackMagic
            /// </summary>
            /// <param name="Address">Целевой адресс</param>
            /// <param name="ValuePtr">Указатель на наше число</param>
            /// <param name="Size">Необходимое количество байт</param>
            /// <param name="ReadMode">Читаем или записываем?</param>
            public static bool ReadWriteBytes(ulong Address, void* ValuePtr, UInt32 Size, bool ReadMode)
            {
                var Request = new RW_BYTES_REQUEST();
                Request.SourceAddress = Address;
                Request.HostAddress = (UInt64)(ValuePtr);
                Request.Size = Size;
                return DeviceIoControl(ReadMode ? IOCTL_READ_BYTES : IOCTL_WRITE_BYTES, (UIntPtr)(&Request), sizeof(RW_BYTES_REQUEST));
            }
        }
    }
}
