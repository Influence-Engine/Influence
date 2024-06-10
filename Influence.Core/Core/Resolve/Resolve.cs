using Silk.NET.OpenGL;
using System;
using System.Runtime.InteropServices;


namespace Influence
{
    public class Resolve
    {
        public static void Log(string message, ResolveLevel level)
        {
            switch (level)
            {
                case ResolveLevel.None: Log(message); break;
                case ResolveLevel.Warning: LogWarning(message); break;
                case ResolveLevel.Error: LogError(message); break;
                case ResolveLevel.Info: Info(message); break;
            }
        }

        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format($"[Log] {message}"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format($"[Warning] {message}"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format($"[Error] {message}"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format($"[Info] {message}"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal delegate void DebugOutputDelegate(GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userParam);
        internal static void DebugOpenGL(GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userParam)
        {
            if(source != GLEnum.NoError)
            {
                string debugMessage = Marshal.PtrToStringAnsi(message);
                ResolveLevel resolveLevel = GetResolveLevelFromSeverity(severity);
                Log($"OpenGL Error:  {debugMessage}", resolveLevel);
            }
        }

        static ResolveLevel GetResolveLevelFromSeverity(GLEnum severity)
        {
            switch (severity)
            {
                default:
                    return ResolveLevel.None;
                case GLEnum.DebugSeverityLow:
                case GLEnum.DebugSeverityMedium:
                    return ResolveLevel.Warning;
                case GLEnum.DebugSeverityHigh:
                    return ResolveLevel.Error;
                case GLEnum.DebugSeverityNotification:
                    return ResolveLevel.Info;
            }
        }
    }
}
