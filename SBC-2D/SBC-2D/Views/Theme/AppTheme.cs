using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

//M3 的核心概念就一句話：
//「每個顏色都有對應的 On 顏色」

//Primary / On Primary → 按鈕配對
//Surface / On Surface → 卡片配對
//Background → Form 底色


namespace SBC_2D.Views
{
    public static class AppTheme
    {
        // ── 背景層 ───────────────────────────────
        public static Color Background = MaterialColors.BlueGrey50; //單元件適合用，不承載內容
        public static Color Surface = Color.White; //有承載內容的背景就用這個，資訊卡等等
        public static Color SurfaceVariant = MaterialColors.Indigo100; //如果承載內容背景需要顏色變化

        // ── 主色配對 ────────────────────────────
        public static Color Primary = MaterialColors.Indigo500; //主強調色
        public static Color OnPrimary = Color.White; //次強調色

        public static Color PrimaryContainer = MaterialColors.Indigo200; //承載Primary的背景主強調色
        public static Color OnPrimaryContainer = MaterialColors.Indigo900; //承載Primary的背景次強調色

        // ── 文字（放在 Surface 上）───────────────
        public static Color OnSurface = MaterialColors.BlueGrey900;
        public static Color OnSurfaceVariant = MaterialColors.BlueGrey600;

        // 字型
        public static readonly Font FontProject = new Font("Segoe UI", 13f, FontStyle.Bold);
        public static readonly Font FontCardTitle = new Font("Segoe UI", 10f, FontStyle.Bold);
        public static readonly Font FontCardSub = new Font("Segoe UI", 9f);
        public static readonly Font FontButton = new Font("Segoe UI", 10f, FontStyle.Bold);

        //版型
        // ── 間距 Token ────────────────────────────
        public static class Spacing
        {
            public const int XS = 4;  // 最小間距
            public const int SM = 8;  // 小間距（控件內 Padding）
            public const int MD = 16;  // 中間距（控件之間）
            public const int LG = 24;  // 大間距（區塊之間）
            public const int XL = 32;  // 最大間距（區域之間）
        }

        // ── 控件尺寸 Token ────────────────────────
        public static class Size_
        {
            public const int ButtonHeight = 32;
            public const int InputHeight = 32;
            public const int ComboHeight = 32;
            public const int LabelHeight = 20;
            public const int PanelPadding = 8;
        }

        // ── 對齊工具 ──────────────────────────────
        public static void AlignVerticalCenter(Control parent, params Control[] controls)
        {
            int centerY = parent.ClientSize.Height / 2;
            foreach (Control ctrl in controls)
                ctrl.Top = centerY - ctrl.Height / 2;
        }

        public static void AlignLeft(int x, params Control[] controls)
        {
            foreach (Control ctrl in controls)
                ctrl.Left = x;
        }

        public static void StackHorizontal(int startX, int gap, params Control[] controls)
        {
            int x = startX;
            foreach (Control ctrl in controls)
            {
                ctrl.Left = x;
                x += ctrl.Width + gap;
            }
        }

        public static void StackVertical(int startY, int gap, params Control[] controls)
        {
            int y = startY;
            foreach (Control ctrl in controls)
            {
                ctrl.Top = y;
                y += ctrl.Height + gap;
            }
        }

        // ── ApplyTheme ───────────────
        public static void ApplyForm(Form form)
        {
            form.BackColor = Surface;
            form.ForeColor = OnSurface;
        }

        public static void ApplyContant(Control control)
        {
            control.BackColor = Surface;
            control.Padding = new Padding(4);
        }

        public static void ApplyTopbar(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                Rectangle rect = panel.ClientRectangle;

                using (LinearGradientBrush brush =
                    new LinearGradientBrush(rect, Color.Empty, Color.Empty, LinearGradientMode.ForwardDiagonal))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[]
                    {
                        MaterialColors.Indigo500,
                        MaterialColors.Indigo300,
                    };

                    blend.Positions = new float[]
                    {
                        0f, // 起點
                        1f  // 終點
                    };

                    brush.InterpolationColors = blend;

                    e.Graphics.FillRectangle(brush, rect);
                }
            };

            foreach (Control control in panel.Controls.OfType<Control>())
            {
                if (control is Label label)
                {
                    ApplyInfoCard(label);
                    ApplyInfoCardTitle(label);
                    ApplyInfoCardValue(label);
                }

                if (control is Button button)
                {
                    ApplyButton(button);
                }
            }
        }

        public static void ApplyBottombar(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                Rectangle rect = panel.ClientRectangle;

                using (LinearGradientBrush brush =
                    new LinearGradientBrush(rect, Color.Empty, Color.Empty, LinearGradientMode.ForwardDiagonal))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[]
                    {
                        MaterialColors.Indigo500,
                        MaterialColors.Indigo300,
                    };

                    blend.Positions = new float[]
                    {
                        0f, // 起點
                        1f  // 終點
                    };

                    brush.InterpolationColors = blend;

                    e.Graphics.FillRectangle(brush, rect);
                }
            };

            foreach (Control control in panel.Controls.OfType<Control>())
            {
                if (control is Button button)
                {
                    ApplyButton(button);
                }
            }
        }

        public static void ApplyInfoCard(Label label)
        {
            label.BackColor = Surface;
        }

        public static void ApplyInfoCardTitle(Label label)
        {
            label.Font = FontCardTitle;
            label.ForeColor = OnSurface;
        }

        public static void ApplyInfoCardValue(Label label)
        {
            label.Font = FontCardSub;
            label.ForeColor = OnSurface;
        }

        public static void ApplyTextbox(Panel panel, TextBox textBox)
        {
            textBox.BackColor = Surface;
            textBox.ForeColor = OnSurface;
            textBox.BorderStyle = BorderStyle.None;
            int textHeight = TextRenderer.MeasureText("Ag", textBox.Font).Height;
            int topPadding = (panel.Height - textHeight) / 2;
            textBox.Top = topPadding;
            panel.Paint -= Panel_TextboxBorder;
            panel.Paint += Panel_TextboxBorder;
        }

        private static void Panel_TextboxBorder(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            Rectangle rect = panel.ClientRectangle;

            using (Pen pen = new Pen(Primary, 1))
                e.Graphics.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
        }

        public static void ApplyButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Surface;
            button.ForeColor = OnSurface;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Primary;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void ApplyComboBox(Panel panel, ComboBox comboBox)
        {
            comboBox.BackColor = Surface;
            comboBox.ForeColor = OnSurface;
            comboBox.Font = FontCardSub;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Left = 1;
            comboBox.Width = panel.Width - 4;
            comboBox.Top = (panel.Height - comboBox.Height) / 2;

            panel.Resize += (s, e) =>
            {
                comboBox.Width = panel.Width - 2;
                comboBox.Top = (panel.Height - comboBox.Height) / 2;
            };

            void OnPaint(object s, PaintEventArgs e)
            {
                bool isActive = comboBox.Focused || comboBox.DroppedDown;
                Color border = isActive ? Primary : PrimaryContainer;
                int width = isActive ? 2 : 1;

                using (Pen pen = new Pen(border, width))
                    e.Graphics.DrawRectangle(pen, 0, 0,
                        panel.Width - 1, panel.Height - 1);
            }

            panel.Paint += OnPaint;
            comboBox.Disposed += (s, e) => panel.Paint -= OnPaint;
        }

        public static void ApplyGroupBox(GroupBox groupBox)
        {
            groupBox.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.Clear(groupBox.BackColor);

                // 測量文字
                Size textSize = TextRenderer.MeasureText(groupBox.Text, groupBox.Font);
                Rectangle textRect = new Rectangle(8, 0, textSize.Width, textSize.Height);

                // 邊框矩形
                Rectangle rect = groupBox.ClientRectangle;
                int top = textRect.Height / 2;
                int left = 0;
                int right = rect.Width - 1;
                int bottom = rect.Height - 1;

                using (Pen pen = new Pen(PrimaryContainer, 2))
                {
                    // 上邊框：左側線段
                    g.DrawLine(pen, left, top, textRect.Left - 2, top);
                    // 上邊框：右側線段
                    g.DrawLine(pen, textRect.Right + 2, top, right, top);
                    // 左邊
                    g.DrawLine(pen, left + 1, top, left, bottom);
                    // 右邊
                    g.DrawLine(pen, right, top, right, bottom);
                    // 下邊
                    g.DrawLine(pen, left, bottom, right, bottom);
                }

                // 畫文字
                TextRenderer.DrawText(g, groupBox.Text, groupBox.Font, textRect, groupBox.ForeColor);
            };
        }

    }
}
