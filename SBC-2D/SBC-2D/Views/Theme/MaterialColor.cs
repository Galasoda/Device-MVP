using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views
{
    public static class MaterialColors
    {
        // ───────────── Red 紅色系 ─────────────
        public static readonly Color Red50 = ColorTranslator.FromHtml("#FFEBEE");
        public static readonly Color Red100 = ColorTranslator.FromHtml("#FFCDD2");
        public static readonly Color Red200 = ColorTranslator.FromHtml("#EF9A9A");
        public static readonly Color Red300 = ColorTranslator.FromHtml("#E57373");
        public static readonly Color Red400 = ColorTranslator.FromHtml("#EF5350");
        public static readonly Color Red500 = ColorTranslator.FromHtml("#F44336");
        public static readonly Color Red600 = ColorTranslator.FromHtml("#E53935");
        public static readonly Color Red700 = ColorTranslator.FromHtml("#D32F2F");
        public static readonly Color Red800 = ColorTranslator.FromHtml("#C62828");
        public static readonly Color Red900 = ColorTranslator.FromHtml("#B71C1C");
        public static readonly Color RedA100 = ColorTranslator.FromHtml("#FF8A80");
        public static readonly Color RedA200 = ColorTranslator.FromHtml("#FF5252");
        public static readonly Color RedA400 = ColorTranslator.FromHtml("#FF1744");
        public static readonly Color RedA700 = ColorTranslator.FromHtml("#D50000");

        // ───────────── Pink 粉紅色系 ─────────────
        public static readonly Color Pink50 = ColorTranslator.FromHtml("#FCE4EC");
        public static readonly Color Pink100 = ColorTranslator.FromHtml("#F8BBD0");
        public static readonly Color Pink200 = ColorTranslator.FromHtml("#F48FB1");
        public static readonly Color Pink300 = ColorTranslator.FromHtml("#F06292");
        public static readonly Color Pink400 = ColorTranslator.FromHtml("#EC407A");
        public static readonly Color Pink500 = ColorTranslator.FromHtml("#E91E63");
        public static readonly Color Pink600 = ColorTranslator.FromHtml("#D81B60");
        public static readonly Color Pink700 = ColorTranslator.FromHtml("#C2185B");
        public static readonly Color Pink800 = ColorTranslator.FromHtml("#AD1457");
        public static readonly Color Pink900 = ColorTranslator.FromHtml("#880E4F");
        public static readonly Color PinkA100 = ColorTranslator.FromHtml("#FF80AB");
        public static readonly Color PinkA200 = ColorTranslator.FromHtml("#FF4081");
        public static readonly Color PinkA400 = ColorTranslator.FromHtml("#F50057");
        public static readonly Color PinkA700 = ColorTranslator.FromHtml("#C51162");

        // ───────────── Purple 紫色系 ─────────────
        public static readonly Color Purple50 = ColorTranslator.FromHtml("#F3E5F5");
        public static readonly Color Purple100 = ColorTranslator.FromHtml("#E1BEE7");
        public static readonly Color Purple200 = ColorTranslator.FromHtml("#CE93D8");
        public static readonly Color Purple300 = ColorTranslator.FromHtml("#BA68C8");
        public static readonly Color Purple400 = ColorTranslator.FromHtml("#AB47BC");
        public static readonly Color Purple500 = ColorTranslator.FromHtml("#9C27B0");
        public static readonly Color Purple600 = ColorTranslator.FromHtml("#8E24AA");
        public static readonly Color Purple700 = ColorTranslator.FromHtml("#7B1FA2");
        public static readonly Color Purple800 = ColorTranslator.FromHtml("#6A1B9A");
        public static readonly Color Purple900 = ColorTranslator.FromHtml("#4A148C");
        public static readonly Color PurpleA100 = ColorTranslator.FromHtml("#EA80FC");
        public static readonly Color PurpleA200 = ColorTranslator.FromHtml("#E040FB");
        public static readonly Color PurpleA400 = ColorTranslator.FromHtml("#D500F9");
        public static readonly Color PurpleA700 = ColorTranslator.FromHtml("#AA00FF");

        // ───────────── DeepPurple 深紫色系 ─────────────
        public static readonly Color DeepPurple50 = ColorTranslator.FromHtml("#EDE7F6");
        public static readonly Color DeepPurple100 = ColorTranslator.FromHtml("#D1C4E9");
        public static readonly Color DeepPurple200 = ColorTranslator.FromHtml("#B39DDB");
        public static readonly Color DeepPurple300 = ColorTranslator.FromHtml("#9575CD");
        public static readonly Color DeepPurple400 = ColorTranslator.FromHtml("#7E57C2");
        public static readonly Color DeepPurple500 = ColorTranslator.FromHtml("#673AB7");
        public static readonly Color DeepPurple600 = ColorTranslator.FromHtml("#5E35B1");
        public static readonly Color DeepPurple700 = ColorTranslator.FromHtml("#512DA8");
        public static readonly Color DeepPurple800 = ColorTranslator.FromHtml("#4527A0");
        public static readonly Color DeepPurple900 = ColorTranslator.FromHtml("#311B92");
        public static readonly Color DeepPurpleA100 = ColorTranslator.FromHtml("#B388FF");
        public static readonly Color DeepPurpleA200 = ColorTranslator.FromHtml("#7C4DFF");
        public static readonly Color DeepPurpleA400 = ColorTranslator.FromHtml("#651FFF");
        public static readonly Color DeepPurpleA700 = ColorTranslator.FromHtml("#6200EA");

        // ───────────── Indigo 靛藍色系 ─────────────
        public static readonly Color Indigo50 = ColorTranslator.FromHtml("#E8EAF6");
        public static readonly Color Indigo100 = ColorTranslator.FromHtml("#C5CAE9");
        public static readonly Color Indigo200 = ColorTranslator.FromHtml("#9FA8DA");
        public static readonly Color Indigo300 = ColorTranslator.FromHtml("#7986CB");
        public static readonly Color Indigo400 = ColorTranslator.FromHtml("#5C6BC0");
        public static readonly Color Indigo500 = ColorTranslator.FromHtml("#3F51B5");
        public static readonly Color Indigo600 = ColorTranslator.FromHtml("#3949AB");
        public static readonly Color Indigo700 = ColorTranslator.FromHtml("#303F9F");
        public static readonly Color Indigo800 = ColorTranslator.FromHtml("#283593");
        public static readonly Color Indigo900 = ColorTranslator.FromHtml("#1A237E");
        public static readonly Color IndigoA100 = ColorTranslator.FromHtml("#8C9EFF");
        public static readonly Color IndigoA200 = ColorTranslator.FromHtml("#536DFE");
        public static readonly Color IndigoA400 = ColorTranslator.FromHtml("#3D5AFE");
        public static readonly Color IndigoA700 = ColorTranslator.FromHtml("#304FFE");

        // ───────────── Blue 藍色系 ─────────────
        public static readonly Color Blue50 = ColorTranslator.FromHtml("#E3F2FD");
        public static readonly Color Blue100 = ColorTranslator.FromHtml("#BBDEFB");
        public static readonly Color Blue200 = ColorTranslator.FromHtml("#90CAF9");
        public static readonly Color Blue300 = ColorTranslator.FromHtml("#64B5F6");
        public static readonly Color Blue400 = ColorTranslator.FromHtml("#42A5F5");
        public static readonly Color Blue500 = ColorTranslator.FromHtml("#2196F3");
        public static readonly Color Blue600 = ColorTranslator.FromHtml("#1E88E5");
        public static readonly Color Blue700 = ColorTranslator.FromHtml("#1976D2");
        public static readonly Color Blue800 = ColorTranslator.FromHtml("#1565C0");
        public static readonly Color Blue900 = ColorTranslator.FromHtml("#0D47A1");
        public static readonly Color BlueA100 = ColorTranslator.FromHtml("#82B1FF");
        public static readonly Color BlueA200 = ColorTranslator.FromHtml("#448AFF");
        public static readonly Color BlueA400 = ColorTranslator.FromHtml("#2979FF");
        public static readonly Color BlueA700 = ColorTranslator.FromHtml("#2962FF");

        // ───────────── LightBlue 淺藍色系 ─────────────
        public static readonly Color LightBlue50 = ColorTranslator.FromHtml("#E1F5FE");
        public static readonly Color LightBlue100 = ColorTranslator.FromHtml("#B3E5FC");
        public static readonly Color LightBlue200 = ColorTranslator.FromHtml("#81D4FA");
        public static readonly Color LightBlue300 = ColorTranslator.FromHtml("#4FC3F7");
        public static readonly Color LightBlue400 = ColorTranslator.FromHtml("#29B6F6");
        public static readonly Color LightBlue500 = ColorTranslator.FromHtml("#03A9F4");
        public static readonly Color LightBlue600 = ColorTranslator.FromHtml("#039BE5");
        public static readonly Color LightBlue700 = ColorTranslator.FromHtml("#0288D1");
        public static readonly Color LightBlue800 = ColorTranslator.FromHtml("#0277BD");
        public static readonly Color LightBlue900 = ColorTranslator.FromHtml("#01579B");
        public static readonly Color LightBlueA100 = ColorTranslator.FromHtml("#80D8FF");
        public static readonly Color LightBlueA200 = ColorTranslator.FromHtml("#40C4FF");
        public static readonly Color LightBlueA400 = ColorTranslator.FromHtml("#00B0FF");
        public static readonly Color LightBlueA700 = ColorTranslator.FromHtml("#0091EA");

        // ───────────── Cyan 青色系 ─────────────
        public static readonly Color Cyan50 = ColorTranslator.FromHtml("#E0F7FA");
        public static readonly Color Cyan100 = ColorTranslator.FromHtml("#B2EBF2");
        public static readonly Color Cyan200 = ColorTranslator.FromHtml("#80DEEA");
        public static readonly Color Cyan300 = ColorTranslator.FromHtml("#4DD0E1");
        public static readonly Color Cyan400 = ColorTranslator.FromHtml("#26C6DA");
        public static readonly Color Cyan500 = ColorTranslator.FromHtml("#00BCD4");
        public static readonly Color Cyan600 = ColorTranslator.FromHtml("#00ACC1");
        public static readonly Color Cyan700 = ColorTranslator.FromHtml("#0097A7");
        public static readonly Color Cyan800 = ColorTranslator.FromHtml("#00838F");
        public static readonly Color Cyan900 = ColorTranslator.FromHtml("#006064");
        public static readonly Color CyanA100 = ColorTranslator.FromHtml("#84FFFF");
        public static readonly Color CyanA200 = ColorTranslator.FromHtml("#18FFFF");
        public static readonly Color CyanA400 = ColorTranslator.FromHtml("#00E5FF");
        public static readonly Color CyanA700 = ColorTranslator.FromHtml("#00B8D4");

        // ───────────── Teal 藍綠色系 ─────────────
        public static readonly Color Teal50 = ColorTranslator.FromHtml("#E0F2F1");
        public static readonly Color Teal100 = ColorTranslator.FromHtml("#B2DFDB");
        public static readonly Color Teal200 = ColorTranslator.FromHtml("#80CBC4");
        public static readonly Color Teal300 = ColorTranslator.FromHtml("#4DB6AC");
        public static readonly Color Teal400 = ColorTranslator.FromHtml("#26A69A");
        public static readonly Color Teal500 = ColorTranslator.FromHtml("#009688");
        public static readonly Color Teal600 = ColorTranslator.FromHtml("#00897B");
        public static readonly Color Teal700 = ColorTranslator.FromHtml("#00796B");
        public static readonly Color Teal800 = ColorTranslator.FromHtml("#00695C");
        public static readonly Color Teal900 = ColorTranslator.FromHtml("#004D40");
        public static readonly Color TealA100 = ColorTranslator.FromHtml("#A7FFEB");
        public static readonly Color TealA200 = ColorTranslator.FromHtml("#64FFDA");
        public static readonly Color TealA400 = ColorTranslator.FromHtml("#1DE9B6");
        public static readonly Color TealA700 = ColorTranslator.FromHtml("#00BFA5");

        // ───────────── Green 綠色系 ─────────────
        public static readonly Color Green50 = ColorTranslator.FromHtml("#E8F5E9");
        public static readonly Color Green100 = ColorTranslator.FromHtml("#C8E6C9");
        public static readonly Color Green200 = ColorTranslator.FromHtml("#A5D6A7");
        public static readonly Color Green300 = ColorTranslator.FromHtml("#81C784");
        public static readonly Color Green400 = ColorTranslator.FromHtml("#66BB6A");
        public static readonly Color Green500 = ColorTranslator.FromHtml("#4CAF50");
        public static readonly Color Green600 = ColorTranslator.FromHtml("#43A047");
        public static readonly Color Green700 = ColorTranslator.FromHtml("#388E3C");
        public static readonly Color Green800 = ColorTranslator.FromHtml("#2E7D32");
        public static readonly Color Green900 = ColorTranslator.FromHtml("#1B5E20");
        public static readonly Color GreenA100 = ColorTranslator.FromHtml("#B9F6CA");
        public static readonly Color GreenA200 = ColorTranslator.FromHtml("#69F0AE");
        public static readonly Color GreenA400 = ColorTranslator.FromHtml("#00E676");
        public static readonly Color GreenA700 = ColorTranslator.FromHtml("#00C853");

        // ───────────── LightGreen 淺綠色系 ─────────────
        public static readonly Color LightGreen50 = ColorTranslator.FromHtml("#F1F8E9");
        public static readonly Color LightGreen100 = ColorTranslator.FromHtml("#DCEDC8");
        public static readonly Color LightGreen200 = ColorTranslator.FromHtml("#C5E1A5");
        public static readonly Color LightGreen300 = ColorTranslator.FromHtml("#AED581");
        public static readonly Color LightGreen400 = ColorTranslator.FromHtml("#9CCC65");
        public static readonly Color LightGreen500 = ColorTranslator.FromHtml("#8BC34A");
        public static readonly Color LightGreen600 = ColorTranslator.FromHtml("#7CB342");
        public static readonly Color LightGreen700 = ColorTranslator.FromHtml("#689F38");
        public static readonly Color LightGreen800 = ColorTranslator.FromHtml("#558B2F");
        public static readonly Color LightGreen900 = ColorTranslator.FromHtml("#33691E");
        public static readonly Color LightGreenA100 = ColorTranslator.FromHtml("#CCFF90");
        public static readonly Color LightGreenA200 = ColorTranslator.FromHtml("#B2FF59");
        public static readonly Color LightGreenA400 = ColorTranslator.FromHtml("#76FF03");
        public static readonly Color LightGreenA700 = ColorTranslator.FromHtml("#64DD17");

        // ───────────── Lime 萊姆色系 ─────────────
        public static readonly Color Lime50 = ColorTranslator.FromHtml("#F9FBE7");
        public static readonly Color Lime100 = ColorTranslator.FromHtml("#F0F4C3");
        public static readonly Color Lime200 = ColorTranslator.FromHtml("#E6EE9C");
        public static readonly Color Lime300 = ColorTranslator.FromHtml("#DCE775");
        public static readonly Color Lime400 = ColorTranslator.FromHtml("#D4E157");
        public static readonly Color Lime500 = ColorTranslator.FromHtml("#CDDC39");
        public static readonly Color Lime600 = ColorTranslator.FromHtml("#C0CA33");
        public static readonly Color Lime700 = ColorTranslator.FromHtml("#AFB42B");
        public static readonly Color Lime800 = ColorTranslator.FromHtml("#9E9D24");
        public static readonly Color Lime900 = ColorTranslator.FromHtml("#827717");
        public static readonly Color LimeA100 = ColorTranslator.FromHtml("#F4FF81");
        public static readonly Color LimeA200 = ColorTranslator.FromHtml("#EEFF41");
        public static readonly Color LimeA400 = ColorTranslator.FromHtml("#C6FF00");
        public static readonly Color LimeA700 = ColorTranslator.FromHtml("#AEEA00");

        // ───────────── Yellow 黃色系 ─────────────
        public static readonly Color Yellow50 = ColorTranslator.FromHtml("#FFFDE7");
        public static readonly Color Yellow100 = ColorTranslator.FromHtml("#FFF9C4");
        public static readonly Color Yellow200 = ColorTranslator.FromHtml("#FFF59D");
        public static readonly Color Yellow300 = ColorTranslator.FromHtml("#FFF176");
        public static readonly Color Yellow400 = ColorTranslator.FromHtml("#FFEE58");
        public static readonly Color Yellow500 = ColorTranslator.FromHtml("#FFEB3B");
        public static readonly Color Yellow600 = ColorTranslator.FromHtml("#FDD835");
        public static readonly Color Yellow700 = ColorTranslator.FromHtml("#FBC02D");
        public static readonly Color Yellow800 = ColorTranslator.FromHtml("#F9A825");
        public static readonly Color Yellow900 = ColorTranslator.FromHtml("#F57F17");
        public static readonly Color YellowA100 = ColorTranslator.FromHtml("#FFFF8D");
        public static readonly Color YellowA200 = ColorTranslator.FromHtml("#FFFF00");
        public static readonly Color YellowA400 = ColorTranslator.FromHtml("#FFEA00");
        public static readonly Color YellowA700 = ColorTranslator.FromHtml("#FFD600");

        // ───────────── Amber 琥珀色系 ─────────────
        public static readonly Color Amber50 = ColorTranslator.FromHtml("#FFF8E1");
        public static readonly Color Amber100 = ColorTranslator.FromHtml("#FFECB3");
        public static readonly Color Amber200 = ColorTranslator.FromHtml("#FFE082");
        public static readonly Color Amber300 = ColorTranslator.FromHtml("#FFD54F");
        public static readonly Color Amber400 = ColorTranslator.FromHtml("#FFCA28");
        public static readonly Color Amber500 = ColorTranslator.FromHtml("#FFC107");
        public static readonly Color Amber600 = ColorTranslator.FromHtml("#FFB300");
        public static readonly Color Amber700 = ColorTranslator.FromHtml("#FFA000");
        public static readonly Color Amber800 = ColorTranslator.FromHtml("#FF8F00");
        public static readonly Color Amber900 = ColorTranslator.FromHtml("#FF6F00");
        public static readonly Color AmberA100 = ColorTranslator.FromHtml("#FFE57F");
        public static readonly Color AmberA200 = ColorTranslator.FromHtml("#FFD740");
        public static readonly Color AmberA400 = ColorTranslator.FromHtml("#FFC400");
        public static readonly Color AmberA700 = ColorTranslator.FromHtml("#FFAB00");

        // ───────────── Orange 橙色系 ─────────────
        public static readonly Color Orange50 = ColorTranslator.FromHtml("#FFF3E0");
        public static readonly Color Orange100 = ColorTranslator.FromHtml("#FFE0B2");
        public static readonly Color Orange200 = ColorTranslator.FromHtml("#FFCC80");
        public static readonly Color Orange300 = ColorTranslator.FromHtml("#FFB74D");
        public static readonly Color Orange400 = ColorTranslator.FromHtml("#FFA726");
        public static readonly Color Orange500 = ColorTranslator.FromHtml("#FF9800");
        public static readonly Color Orange600 = ColorTranslator.FromHtml("#FB8C00");
        public static readonly Color Orange700 = ColorTranslator.FromHtml("#F57C00");
        public static readonly Color Orange800 = ColorTranslator.FromHtml("#EF6C00");
        public static readonly Color Orange900 = ColorTranslator.FromHtml("#E65100");
        public static readonly Color OrangeA100 = ColorTranslator.FromHtml("#FFD180");
        public static readonly Color OrangeA200 = ColorTranslator.FromHtml("#FFAB40");
        public static readonly Color OrangeA400 = ColorTranslator.FromHtml("#FF9100");
        public static readonly Color OrangeA700 = ColorTranslator.FromHtml("#FF6D00");

        // ───────────── DeepOrange 深橙色系 ─────────────
        public static readonly Color DeepOrange50 = ColorTranslator.FromHtml("#FBE9E7");
        public static readonly Color DeepOrange100 = ColorTranslator.FromHtml("#FFCCBC");
        public static readonly Color DeepOrange200 = ColorTranslator.FromHtml("#FFAB91");
        public static readonly Color DeepOrange300 = ColorTranslator.FromHtml("#FF8A65");
        public static readonly Color DeepOrange400 = ColorTranslator.FromHtml("#FF7043");
        public static readonly Color DeepOrange500 = ColorTranslator.FromHtml("#FF5722");
        public static readonly Color DeepOrange600 = ColorTranslator.FromHtml("#F4511E");
        public static readonly Color DeepOrange700 = ColorTranslator.FromHtml("#E64A19");
        public static readonly Color DeepOrange800 = ColorTranslator.FromHtml("#D84315");
        public static readonly Color DeepOrange900 = ColorTranslator.FromHtml("#BF360C");
        public static readonly Color DeepOrangeA100 = ColorTranslator.FromHtml("#FF9E80");
        public static readonly Color DeepOrangeA200 = ColorTranslator.FromHtml("#FF6E40");
        public static readonly Color DeepOrangeA400 = ColorTranslator.FromHtml("#FF3D00");
        public static readonly Color DeepOrangeA700 = ColorTranslator.FromHtml("#DD2C00");

        // ───────────── Brown 棕色系 ─────────────
        public static readonly Color Brown50 = ColorTranslator.FromHtml("#EFEBE9");
        public static readonly Color Brown100 = ColorTranslator.FromHtml("#D7CCC8");
        public static readonly Color Brown200 = ColorTranslator.FromHtml("#BCAAA4");
        public static readonly Color Brown300 = ColorTranslator.FromHtml("#A1887F");
        public static readonly Color Brown400 = ColorTranslator.FromHtml("#8D6E63");
        public static readonly Color Brown500 = ColorTranslator.FromHtml("#795548");
        public static readonly Color Brown600 = ColorTranslator.FromHtml("#6D4C41");
        public static readonly Color Brown700 = ColorTranslator.FromHtml("#5D4037");
        public static readonly Color Brown800 = ColorTranslator.FromHtml("#4E342E");
        public static readonly Color Brown900 = ColorTranslator.FromHtml("#3E2723");

        // ───────────── Grey 灰色系 ─────────────
        public static readonly Color Grey50 = ColorTranslator.FromHtml("#FAFAFA");
        public static readonly Color Grey100 = ColorTranslator.FromHtml("#F5F5F5");
        public static readonly Color Grey200 = ColorTranslator.FromHtml("#EEEEEE");
        public static readonly Color Grey300 = ColorTranslator.FromHtml("#E0E0E0");
        public static readonly Color Grey400 = ColorTranslator.FromHtml("#BDBDBD");
        public static readonly Color Grey500 = ColorTranslator.FromHtml("#9E9E9E");
        public static readonly Color Grey600 = ColorTranslator.FromHtml("#757575");
        public static readonly Color Grey700 = ColorTranslator.FromHtml("#616161");
        public static readonly Color Grey800 = ColorTranslator.FromHtml("#424242");
        public static readonly Color Grey900 = ColorTranslator.FromHtml("#212121");

        // ───────────── BlueGrey 藍灰色系 ─────────────
        public static readonly Color BlueGrey50 = ColorTranslator.FromHtml("#ECEFF1");
        public static readonly Color BlueGrey100 = ColorTranslator.FromHtml("#CFD8DC");
        public static readonly Color BlueGrey200 = ColorTranslator.FromHtml("#B0BEC5");
        public static readonly Color BlueGrey300 = ColorTranslator.FromHtml("#90A4AE");
        public static readonly Color BlueGrey400 = ColorTranslator.FromHtml("#78909C");
        public static readonly Color BlueGrey500 = ColorTranslator.FromHtml("#607D8B");
        public static readonly Color BlueGrey600 = ColorTranslator.FromHtml("#546E7A");
        public static readonly Color BlueGrey700 = ColorTranslator.FromHtml("#455A64");
        public static readonly Color BlueGrey800 = ColorTranslator.FromHtml("#37474F");
        public static readonly Color BlueGrey900 = ColorTranslator.FromHtml("#263238");
    }
}

