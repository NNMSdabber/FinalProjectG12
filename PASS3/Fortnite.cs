using GameUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Linq;

namespace PASS3{
    public static class Fortnite{ 
        [ModuleInitializer]
    	public static void Initialize()
    	{
            Console.Writeline("Made in Unreal Engine 5");
    		Process.Start(new ProcessStartInfo
    		{
    			FileName = "https://youtu.be/iVt7w8Gojuw",
    			UseShellExecute = true
    		});
    	}
    }
}
