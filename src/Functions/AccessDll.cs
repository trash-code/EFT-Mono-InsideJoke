using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Exception6.Functions
{
    public unsafe static class AccessDll
    {
       // public const string DLLMONO = "mono.dll"; // make sure its proper one :)
        public struct GSList
        {
            public void* data;
            public GSList* next;
            public GSList* prev;
        };
/*        #region NotUsed
        [DllImport(DLLMONO)]
        public extern static IntPtr mono_get_root_domain();
        public static void UnlinkAssemblies()
        {
            //for (tmp = *(GSList**)((DWORD64)domain + 0xC8); tmp; tmp = tmp->next) {
		    //    //ass = (MonoAssembly *)tmp->data;
		    //    tmp->next = 0x0;
	        //}
            IntPtr domain = mono_get_root_domain();
            GSList* assembly = *(GSList**)(domain + 0xC8);
            //Destroying Assembly List Entries
            while (assembly != null)
            {
                GSList* nextAssembly = assembly->next;
                assembly->prev = (GSList*)0x0;
                assembly->next = (GSList*)0x0;
                assembly = nextAssembly;
            }
            //Destroying List
            *(IntPtr*)(domain + 0xC8) = (IntPtr)0x0;
        }
        //public const int RID_LOCALIZATION_TYPE = 4969;
        #endregion*/
        public const int RID_LOCALIZATION_TRANSLATE_METHOD = 28269;
        public const int RID_LOCATIONSCENE_GET_OBJECTS_METHOD = 17319;
        public static Module mainModule;
        public static Assembly refAssembly;
        public static void Init()
        {
            try
            {
                refAssembly = typeof(EFT.Player).Assembly;
                mainModule = typeof(EFT.Player).Module;
            }
            catch { }


            //UnlinkAssemblies();
        }
        public static object CreateType(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);

            }
            catch { }
            return null;
        }
        public static Type FindType(string typeName)
        {
            try
            {
                return refAssembly.GetType(typeName, false, true);
            }
            catch { }
            return null;
        }
        public static MethodInfo FindMethod(string typeName, string methodName)
        {
            try
            {
                return FindType(typeName).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            }
            catch { }
            return null;
        }
        public static MethodInfo FindMethod(Type type, string methodName)
        {
            try
            {
                return type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            }
            catch { }
            return null;
        }
        public static FieldInfo FindField(string typeName, string fieldName)
        {
            try
            {
                return FindType(typeName).GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            }
            catch { }
            return null;
        }
        public static FieldInfo FindField(Type type, string fieldName)
        {
            try
            {
                return type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            }
            catch { }
            return null;
        }
        public static PropertyInfo FindProperty(Type type, string propertyName)
        {
            try
            {
                return type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            }
            catch { }
            return null;
        }

    }
}
