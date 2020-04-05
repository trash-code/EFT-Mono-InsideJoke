using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace Exception6
{
    //[ObfuscationAttribute(Exclude = true, ApplyToMembers = true, Feature = "-rename")]
    [ObfuscationAttribute(Exclude = true)]
    public static class Init
    {
        [ObfuscationAttribute(Exclude = true)]
        public static void Main() {
            try
            {
                GameObject main = new GameObject();
                main.AddComponent<Menu>();
                GameObject.DontDestroyOnLoad(main);
            } catch { }
        }
    }
}
