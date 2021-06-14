using System;
using System.Collections.Generic;
using System.IO;

namespace Jvedio.Core
{
    /// <summary>
    /// 主题加载
    /// </summary>
    public static class ThemeLoader
    {
        /**
         * 【初始化的过程】：
         * 1. 扫描 Themes 下的所有目录，每个目录即为一个主题
         * 2. 验证每个主题的合法性
         * 3. 加载：键值对 => key=主题名，value=目录名
         *
         */

        /**
         * 【主题的目录结构】：
         * Themes
         *      - 主题1
         *          ReadMe.md
         *          index.json ：用于描述主题的元信息，包括 主题名称、来源、发布日期、作者、文件夹大小
         *          theme.css：主题的核心文件
         *          fonts
         *              - fonts1.otf：字体文件
         *      - 主题2
         *      - ...
         */

        private static string ThemeName { get; set; }
        private static Dictionary<string, string> ThemeMaps { get; set; }

        private static readonly string ThemePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Themes");

        public static List<string> getAllConfigFiles(string path)
        {
            return new List<string>();
        }

        private static void initThemeMaps()
        {
            FileHelper.TryCreateDir(ThemePath);
        }

        public static void loadThemeByPathName(string pathName)
        {
        }
    }
}