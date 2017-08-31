using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new Form1());
    }
}
class Form1 : Form
{
    const int BLOCK_SIZE = 32;
    Timer timer;
    static int Power = 9;
    int Ram;
    int animewai = 0;
    int NeBl;
    int GAMESET = 0;
    int x = 32 * 4;  //  x 座標
    int y = 0;  //  y 座標
    static int[,] BS = new int[9, 16];
    static int animech = 1;
    int Point = 0;
    int Stop = 0;
    int Counttime;
    int downspeed = 2;
    static string hiscore;
    static int Chain = 0;
    static int[] anime = new int[150];
    public Image Uesi;
    public Image Himi;
    public Image Uehi;
    public Image Uemi;
    public Image Simi;
    public Image Sihi;
    public Image Fire;
    public Image Juji;
    public Image Yuki;
    public Image Bom;


    public Image[] Uesi1 = new Image[9];
    public Image[] Himi1 = new Image[9];
    public Image[] Uehi1 = new Image[9];
    public Image[] Uemi1 = new Image[9];
    public Image[] Simi1 = new Image[9];
    public Image[] Sihi1 = new Image[9];
    public Image[] Juji1 = new Image[15];

    public Form1()
    {
        string dir = System.IO.Directory.GetCurrentDirectory();       //私用ディレクトリを取得
        Random aRandom = new System.Random();
        Ram = aRandom.Next(1, 10);
        Random dRandom = new System.Random();
        NeBl = aRandom.Next(1, 10);

        timer = new Timer()
        {
            Interval = 30,
            Enabled = true,
        };

        this.KeyDown += new KeyEventHandler(Form1_KeyDown);

        timer.Tick += new EventHandler(timer_Tick);

        this.DoubleBuffered = true;  // ダブルバッファリング
        this.BackColor = SystemColors.Window;

        this.ClientSize = new Size(BLOCK_SIZE * 15, BLOCK_SIZE * 14);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.TopMost = true;
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
    }
    void timer_Tick(object sender, EventArgs e)
    {
        if (Stop == 1)
        {
            /*  a++;
              if (a > 10)
              {
                  a = 0;

                  //////////////
                  if (Ram < 10) Ram = 10;
                  else if (Ram == 10) Ram = 11;
                  else if (Ram == 11) Ram = 12;
                  else Ram = 10;

                  ///////////////
                  this.Invalidate();  // 再描画を促す
              }*/
        }
        else if (Stop == 2)//アニメーション
        {
            animewai++;
            if (animewai > 2)
            {
                if (anime[animech] == 0)
                {
                    Down();//重力
                    for (int i = 0; i < 150; i++) anime[i] = 0;
                    Stop = 0;
                    animech = 1;
                }
                else if (anime[animech] < 0)
                {

                    int animex = -anime[animech] / 100000;
                    int animey = (-anime[animech] % 100000) / 1000;

                    BS[animex, animey] = 0;
                    animech = animech + 1;
                }
                else
                {
                    int animex = anime[animech] / 100000;
                    int animey = (anime[animech] % 100000) / 1000;
                    if (BS[animex, animey] < 0)
                    {
                        int animeb = anime[animech] % 1000;
                        BS[animex, animey] = animeb + 10;
                    }
                    else
                    {
                        BS[animex, animey] = BS[animex, animey] + 1;
                        if ((BS[animex, animey] - 10) % 8 == 4 || (BS[animex, animey] - 10) % 8 == 7 || (BS[animex, animey] - 10) % 8 == 1)
                        {
                            BS[animex, animey] = 0;
                            animech = animech + 1;
                        }
                    }
                }
                animewai = 0;
            }

            this.Invalidate();  // 再描画を促す
        }
        else
        {
            if (y + BLOCK_SIZE <= BLOCK_SIZE * 14 && BS[x / BLOCK_SIZE, (y + BLOCK_SIZE) / BLOCK_SIZE] == 0)
            {
                Counttime++;
                if (Counttime > 1000 + downspeed * 100)
                {
                    downspeed++;
                    Counttime = 0;
                }
                y = y + downspeed;
            }
            else
            {
                if (GAMESET == 0)
                {
                    if (Ram == 7)
                    {

                        for (int i = 0; i < 150; i++) anime[i] = 0;

                        int hono = 0;
                        Chain = 1;
                        hono = hono + fire(BS[(x / BLOCK_SIZE) + 1, y / BLOCK_SIZE], (x / BLOCK_SIZE) + 1, y / BLOCK_SIZE, 1, 0);
                        if (x / BLOCK_SIZE > 0)
                        {
                            hono = hono + fire(BS[(x / BLOCK_SIZE) - 1, y / BLOCK_SIZE], (x / BLOCK_SIZE) - 1, y / BLOCK_SIZE, -1, 0);
                        }
                        hono = hono + fire(BS[x / BLOCK_SIZE, (y / BLOCK_SIZE) + 1], x / BLOCK_SIZE, (y / BLOCK_SIZE) + 1, 0, 1);
                        if (y / BLOCK_SIZE > 0)
                        {
                            hono = hono + fire(BS[x / BLOCK_SIZE, (y / BLOCK_SIZE) - 1], x / BLOCK_SIZE, (y / BLOCK_SIZE) - 1, 0, -1);
                        }
                        if (hono == 0)
                        {
                            BS[x / BLOCK_SIZE, y / BLOCK_SIZE] = Ram;

                            var sound = new SoundPlayer();                          //プレイヤーを立ち上げ
                            sound.SoundLocation = "date-sound/block.wav";          //ファイルを読み込み
                            sound.Play(); //(カタッという音)

                        }
                        else
                        {
                            int i = Chain + 20 * Chain;//(int)Math.Log(Math.Pow(Chain, 30));
                            Point = Point + i;

                            //アニメ―ション開始終了

                            Stop = 2;
                            var sound = new SoundPlayer();                          //プレイヤーを立ち上げ
                            sound.SoundLocation = "date-sound/fire.wav";          //ファイルを読み込み
                            sound.Play();   //(火が付いた音)
                        }
                    }
                    else
                    {
                        BS[x / BLOCK_SIZE, y / BLOCK_SIZE] = Ram;//Nextの値を入れる


                        var sound = new SoundPlayer();                          //プレイヤーを立ち上げ
                        sound.SoundLocation = "date-sound/block.wav";          //ファイルを読み込み
                        sound.Play(); //(カタッという音)

                    }


                    Ram = NeBl;//新規ブロック
                    Random bRandom = new System.Random();
                    Random cRandom = new System.Random();
                    NeBl = bRandom.Next(1, 4);//////////////////////////////////////////////////////////
                    if (NeBl == 1)
                    {
                        NeBl = bRandom.Next(1, 3);
                    }
                    else if (NeBl == 2)
                    {
                        NeBl = bRandom.Next(3, 7);
                    }
                    else if (NeBl == 3)
                    {
                        NeBl = bRandom.Next(7, 10);
                    }
                    y = -BLOCK_SIZE;
                    x = BLOCK_SIZE * 4;
                }
                else
                {

                    if (int.Parse(hiscore) < Point)
                    {
                        hiscore = Point.ToString();
                        //              string dir = System.IO.Directory.GetCurrentDirectory();
                        using (StreamWriter sr = new StreamWriter("date-txt/hiscore.txt", false))
                        {
                            sr.WriteLine(hiscore);
                        }
                    }
                    BS[x / BLOCK_SIZE, y / BLOCK_SIZE] = 0;
                }
            }
        }
        this.Invalidate();  // 再描画を促す
    }

    void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (GAMESET == 0)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (Stop == 0)
                {
                    Stop = 1;
                }
                else if (Stop == 1)
                {
                    Stop = 0;
                }
            }
            if (Stop == 0 && y > 0)
            {

                if (e.KeyCode == Keys.Z)/////////チェンジ
                {
                    if (Ram == 2)
                    {
                        Ram = 1;
                    }
                    else if (Ram == 6)
                    {
                        Ram = 3;
                    }
                    else if (Ram < 10 && Ram > 6)
                    {
                    }
                    else
                    {
                        Ram = Ram + 1;
                    }
                }
                if (e.KeyCode == Keys.X)/////////チェンジ
                {
                    if (Ram == 1)
                    {
                        Ram = 2;
                    }
                    else if (Ram == 3)
                    {
                        Ram = 6;
                    }
                    else if (Ram > 6)
                    {
                    }
                    else
                    {
                        Ram = Ram - 1;
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (x <= BLOCK_SIZE * (8 - 2) && BS[(x + BLOCK_SIZE) / BLOCK_SIZE, y / BLOCK_SIZE] == 0)
                    {
                        x = x + BLOCK_SIZE;
                        y = y - (int)Math.Log(1 + Point * Point / 1000) * 4 / 5;
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (x > 0)
                    {
                        if (BS[(x - BLOCK_SIZE) / BLOCK_SIZE, y / BLOCK_SIZE] == 0)
                        {
                            x = x - BLOCK_SIZE;
                            y = y - (int)Math.Log(1 + Point * Point / 1000) * 4 / 5;
                        }
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    y = y + 15;
                }
                if (e.KeyCode == Keys.S)
                {
                    if (Power >= 3 && NeBl != 7)
                    {
                        NeBl = 7;
                        Power = Power - 3;
                    }
                }
            }
        }
        if (e.KeyCode == Keys.R)
        {
            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 14; b++)
                {
                    BS[a, b] = 0;
                }
            }
            Ram = NeBl;
            Random bRandom = new System.Random();
            Random cRandom = new System.Random();
            NeBl = bRandom.Next(1, 4);//////////////////////////////////////////////////////////
            if (NeBl == 1)
            {
                NeBl = bRandom.Next(1, 3);
            }
            else if (NeBl == 2)
            {
                NeBl = bRandom.Next(3, 7);
            }
            else if (NeBl == 3)
            {
                NeBl = bRandom.Next(7, 10);
            }
            y = 0;
            x = BLOCK_SIZE * 4;
            downspeed = 2;
            Power = 9;
            Counttime = 0;
            Point = 0;
            GAMESET = 0;
        }
    }
    protected override void OnPaint(PaintEventArgs e)
    {

        //  string dir = System.IO.Directory.GetCurrentDirectory();
        string UESI = System.IO.Path.Combine("date-gazo/uesi.png");
        string HIMI = System.IO.Path.Combine("date-gazo/himi.png");
        string UEHI = System.IO.Path.Combine("date-gazo/uehi.png");
        string UEMI = System.IO.Path.Combine("date-gazo/uemi.png");
        string SIMI = System.IO.Path.Combine("date-gazo/simi.png");
        string SIHI = System.IO.Path.Combine("date-gazo/sihi.png");
        string FIRE = System.IO.Path.Combine("date-gazo/fire.png");
        string JUJI = System.IO.Path.Combine("date-gazo/juji.png");
        string YUKI = System.IO.Path.Combine("date-gazo/yuki.png");
        string BOM = System.IO.Path.Combine("date-gazo/bom.png");

        string[] UESI1 = new string[9];
        string[] HIMI1 = new string[9];
        string[] UEHI1 = new string[9];
        string[] UEMI1 = new string[9];
        string[] SIMI1 = new string[9];
        string[] SIHI1 = new string[9];
        string[] JUJI1 = new string[15];
        for (int fiti = 1; fiti <= 8; fiti++)
        {
            UESI1[fiti] = System.IO.Path.Combine("date-gazo/uesi/uesi" + fiti + ".png");
            HIMI1[fiti] = System.IO.Path.Combine("date-gazo/himi/himi" + fiti + ".png");
            UEHI1[fiti] = System.IO.Path.Combine("date-gazo/uehi/uehi" + fiti + ".png");
            UEMI1[fiti] = System.IO.Path.Combine("date-gazo/uemi/uemi" + fiti + ".png");
            SIMI1[fiti] = System.IO.Path.Combine("date-gazo/simi/simi" + fiti + ".png");
            SIHI1[fiti] = System.IO.Path.Combine("date-gazo/sihi/sihi" + fiti + ".png");

        }
        for (int fiti = 1; fiti <= 14; fiti++)
        {
            JUJI1[fiti] = System.IO.Path.Combine("date-gazo/juji/juji" + fiti + ".png");
        }

        for (int fiti = 1; fiti <= 8; fiti++)
        {
            if (Uesi1[fiti] == null) Uesi1[fiti] = Image.FromFile(UESI1[fiti]);
            if (Himi1[fiti] == null) Himi1[fiti] = Image.FromFile(HIMI1[fiti]);
            if (Uehi1[fiti] == null) Uehi1[fiti] = Image.FromFile(UEHI1[fiti]);
            if (Uemi1[fiti] == null) Uemi1[fiti] = Image.FromFile(UEMI1[fiti]);
            if (Simi1[fiti] == null) Simi1[fiti] = Image.FromFile(SIMI1[fiti]);
            if (Sihi1[fiti] == null) Sihi1[fiti] = Image.FromFile(SIHI1[fiti]);
        }

        for (int fiti = 1; fiti <= 14; fiti++)
        {
            if (Juji1[fiti] == null) Juji1[fiti] = Image.FromFile(JUJI1[fiti]);
        }


        if (Uesi == null) Uesi = Image.FromFile(UESI);
        if (Himi == null) Himi = Image.FromFile(HIMI);
        if (Uehi == null) Uehi = Image.FromFile(UEHI);
        if (Uemi == null) Uemi = Image.FromFile(UEMI);
        if (Simi == null) Simi = Image.FromFile(SIMI);
        if (Sihi == null) Sihi = Image.FromFile(SIHI);
        if (Fire == null) Fire = Image.FromFile(FIRE);
        if (Juji == null) Juji = Image.FromFile(JUJI);
        if (Yuki == null) Yuki = Image.FromFile(YUKI);
        if (Bom == null) Bom = Image.FromFile(BOM);


        base.OnPaint(e);
        Pen blupen = new Pen(Color.Red);
        e.Graphics.DrawLine(blupen, BLOCK_SIZE * 4, 0, BLOCK_SIZE * 5, BLOCK_SIZE);
        e.Graphics.DrawLine(blupen, BLOCK_SIZE * 5, 0, BLOCK_SIZE * 4, BLOCK_SIZE);
        Pen blapen = new Pen(Color.Black);
        e.Graphics.DrawRectangle(blapen, 0, 0, BLOCK_SIZE * 8, BLOCK_SIZE * 14 - 1);

        blupen.Dispose();



        string stp = Point.ToString();
        Font font = new Font("Times New Roman", 30, FontStyle.Regular);
        Font mofo = new Font("Times New Roman", 10, FontStyle.Regular);

        e.Graphics.DrawString(stp, font, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE);
        if (hiscore == null)
        {
            using (StreamReader sr = new StreamReader("date-txt/hiscore.txt"))
            {
                hiscore = sr.ReadLine();
            }
        }
        e.Graphics.DrawString("ハイスコア" + hiscore, mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 3);
        e.Graphics.DrawString("スピード" + downspeed, mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 4);
        e.Graphics.DrawString(Counttime + "/" + (1000 + downspeed * 100), mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 5);
        e.Graphics.DrawString("s…すごい技(あと" + Power / 3 + "回)", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 6);
        e.Graphics.DrawString("→…右移動", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 7);
        e.Graphics.DrawString("←…左移動", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 8);
        e.Graphics.DrawString("↓…加速", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 9);
        e.Graphics.DrawString("z…右回転", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 10);
        e.Graphics.DrawString("x…左回転", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 11);
        e.Graphics.DrawString("space…ポーズ", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 12);
        e.Graphics.DrawString("r…リセット", mofo, SystemBrushes.WindowText, BLOCK_SIZE * 8, BLOCK_SIZE * 13);


        if (NeBl == 1)//Nextブロック
            e.Graphics.DrawImage(Uesi, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 2)
            e.Graphics.DrawImage(Himi, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 3)
            e.Graphics.DrawImage(Uehi, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 4)
            e.Graphics.DrawImage(Uemi, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 5)
            e.Graphics.DrawImage(Simi, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 6)
            e.Graphics.DrawImage(Sihi, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 7)
            e.Graphics.DrawImage(Fire, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 8)
            e.Graphics.DrawImage(Yuki, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);
        else if (NeBl == 9)
            e.Graphics.DrawImage(Bom, BLOCK_SIZE * 8, 0, BLOCK_SIZE, BLOCK_SIZE);

        if (Ram == 1)//使用ブロック
            e.Graphics.DrawImage(Uesi, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 2)
            e.Graphics.DrawImage(Himi, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 3)
            e.Graphics.DrawImage(Uehi, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 4)
            e.Graphics.DrawImage(Uemi, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 5)
            e.Graphics.DrawImage(Simi, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 6)
            e.Graphics.DrawImage(Sihi, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 7)
            e.Graphics.DrawImage(Fire, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 8)
            e.Graphics.DrawImage(Yuki, x, y, BLOCK_SIZE, BLOCK_SIZE);
        else if (Ram == 9)
            e.Graphics.DrawImage(Bom, x, y, BLOCK_SIZE, BLOCK_SIZE);


        for (int a = 0; a < 9; a++)
        {
            for (int b = 0; b < 14; b++)
            {
                if (BS[4, 0] > 0)
                {
                    GAMESET = 1;
                    break;
                }

                if (BS[a, b] == 1 || BS[a, b] == -1)//マップ上のブロック
                    e.Graphics.DrawImage(Uesi, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 2 || BS[a, b] == -2)
                    e.Graphics.DrawImage(Himi, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 3 || BS[a, b] == -3)
                    e.Graphics.DrawImage(Uehi, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 4 || BS[a, b] == -4)
                    e.Graphics.DrawImage(Uemi, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 5 || BS[a, b] == -5)
                    e.Graphics.DrawImage(Simi, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 6 || BS[a, b] == -6)
                    e.Graphics.DrawImage(Sihi, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 7 || BS[a, b] == -7)
                    e.Graphics.DrawImage(Juji, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 8 || BS[a, b] == -8)
                    e.Graphics.DrawImage(Yuki, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 9 || BS[a, b] == -9)
                    e.Graphics.DrawImage(Bom, BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);

                else if (BS[a, b] == 11)
                    e.Graphics.DrawImage(Uesi1[1], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 12)
                    e.Graphics.DrawImage(Uesi1[2], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 13)
                    e.Graphics.DrawImage(Uesi1[3], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 14)
                    e.Graphics.DrawImage(Uesi1[4], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 15)
                    e.Graphics.DrawImage(Uesi1[5], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 16)
                    e.Graphics.DrawImage(Uesi1[6], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 17)
                    e.Graphics.DrawImage(Uesi1[7], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 18)
                    e.Graphics.DrawImage(Uesi1[8], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);

                else if (BS[a, b] == 19)
                    e.Graphics.DrawImage(Himi1[1], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 20)
                    e.Graphics.DrawImage(Himi1[2], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 21)
                    e.Graphics.DrawImage(Himi1[3], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 22)
                    e.Graphics.DrawImage(Himi1[4], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 23)
                    e.Graphics.DrawImage(Himi1[5], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 24)
                    e.Graphics.DrawImage(Himi1[6], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 25)
                    e.Graphics.DrawImage(Himi1[7], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 26)
                    e.Graphics.DrawImage(Himi1[8], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);

                else if (BS[a, b] == 27)
                    e.Graphics.DrawImage(Uehi1[1], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 28)
                    e.Graphics.DrawImage(Uehi1[2], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 29)
                    e.Graphics.DrawImage(Uehi1[3], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 30)
                    e.Graphics.DrawImage(Uehi1[4], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 31)
                    e.Graphics.DrawImage(Uehi1[5], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 32)
                    e.Graphics.DrawImage(Uehi1[6], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 33)
                    e.Graphics.DrawImage(Uehi1[7], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 34)
                    e.Graphics.DrawImage(Uehi1[8], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);


                else if (BS[a, b] == 35)
                    e.Graphics.DrawImage(Uemi1[1], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 36)
                    e.Graphics.DrawImage(Uemi1[2], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 37)
                    e.Graphics.DrawImage(Uemi1[3], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 38)
                    e.Graphics.DrawImage(Uemi1[4], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 39)
                    e.Graphics.DrawImage(Uemi1[5], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 40)
                    e.Graphics.DrawImage(Uemi1[6], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 41)
                    e.Graphics.DrawImage(Uemi1[7], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 42)
                    e.Graphics.DrawImage(Uemi1[8], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);


                else if (BS[a, b] == 43)
                    e.Graphics.DrawImage(Simi1[1], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 44)
                    e.Graphics.DrawImage(Simi1[2], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 45)
                    e.Graphics.DrawImage(Simi1[3], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 46)
                    e.Graphics.DrawImage(Simi1[4], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 47)
                    e.Graphics.DrawImage(Simi1[5], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 48)
                    e.Graphics.DrawImage(Simi1[6], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 49)
                    e.Graphics.DrawImage(Simi1[7], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 50)
                    e.Graphics.DrawImage(Simi1[8], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);

                else if (BS[a, b] == 51)
                    e.Graphics.DrawImage(Sihi1[1], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 52)
                    e.Graphics.DrawImage(Sihi1[2], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 53)
                    e.Graphics.DrawImage(Sihi1[3], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 54)
                    e.Graphics.DrawImage(Sihi1[4], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 55)
                    e.Graphics.DrawImage(Sihi1[5], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 56)
                    e.Graphics.DrawImage(Sihi1[6], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 57)
                    e.Graphics.DrawImage(Sihi1[7], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);
                else if (BS[a, b] == 58)
                    e.Graphics.DrawImage(Sihi1[8], BLOCK_SIZE * a, BLOCK_SIZE * b, BLOCK_SIZE, BLOCK_SIZE);

            }
        }
    }
    static int fire(int z, int x, int y, int a, int b)
    {
        int hono = 0;
        if (z == 1)
            hono = uesi(x, y, a, b);
        else if (z == 2)
            hono = himi(x, y, a, b);
        else if (z == 3)
            hono = uehi(x, y, a, b);
        else if (z == 4)
            hono = uemi(x, y, a, b);
        else if (z == 5)
            hono = simi(x, y, a, b);
        else if (z == 6)
            hono = sihi(x, y, a, b);
        else if (z == 7)
            hono = fipa(x, y, a, b);
        else if (z == 8)
            hono = yuki(x, y, a, b);
        else if (z == 9)
            hono = bom(x, y, a, b);

        return hono;
    }
    static int uesi(int x, int y, int a, int b)
    {
        int hono = 0;
        if (b == -1 || b == 1)
        {
            if (a == b) anime[Chain] = 7 + (x * 100000) + (y * 1000);//真ん中
            else if (b == 1) anime[Chain] = 1 + (x * 100000) + (y * 1000);//上から下
            else if (b == -1) anime[Chain] = 4 + (x * 100000) + (y * 1000);//下から上
            BS[x, y] = -BS[x, y];
            hono = 1;
            Chain++;
            if (y > 0)
            {
                if (BS[x, y - 1] > 0)
                {
                    fire(BS[x, y - 1], x, y - 1, 0, -1);
                }
            }
            if (BS[x, y + 1] > 0)
            {

                fire(BS[x, y + 1], x, y + 1, 0, 1);
            }
        }
        return hono;
    }
    static int himi(int x, int y, int a, int b)
    {
        int hono = 0;
        if (a == -1 || a == 1)
        {
            if (a == b) anime[Chain] = 7 + (8 * 1) + (x * 100000) + (y * 1000);//真ん中
            else if (a == 1) anime[Chain] = 1 + (8 * 1) + (x * 100000) + (y * 1000);//左から右
            else if (a == -1) anime[Chain] = 4 + (8 * 1) + (x * 100000) + (y * 1000);//右から左
            BS[x, y] = -BS[x, y];
            hono = 1;
            Chain++;
            if (x > 0)
            {
                if (BS[x - 1, y] > 0)
                {
                    fire(BS[x - 1, y], x - 1, y, -1, 0);
                }
            }
            if (BS[x + 1, y] > 0)
            {

                fire(BS[x + 1, y], x + 1, y, 1, 0);
            }
        }
        return hono;
    }
    static int uehi(int x, int y, int a, int b)
    {
        int hono = 0;
        if (a == 1 || b == 1)
        {
            if (a == b) anime[Chain] = 7 + (8 * 2) + (x * 100000) + (y * 1000);//真ん中
            else if (b == 1) anime[Chain] = 1 + (8 * 2) + (x * 100000) + (y * 1000);//上から左
            else if (a == 1) anime[Chain] = 4 + (8 * 2) + (x * 100000) + (y * 1000);//左から上
            BS[x, y] = -BS[x, y];
            hono = 1;
            Chain++;
            if (y > 0)
            {
                if (BS[x, y - 1] > 0)
                {
                    fire(BS[x, y - 1], x, y - 1, 0, -1);
                }
            }
            if (x > 0)
            {
                if (BS[x - 1, y] > 0)
                {
                    fire(BS[x - 1, y], x - 1, y, -1, 0);
                }
            }
        }
        return hono;
    }
    static int uemi(int x, int y, int a, int b)
    {
        int hono = 0;
        if (a == -1 || b == 1)
        {
            if (a == b) anime[Chain] = 7 + (8 * 3) + (x * 100000) + (y * 1000);//真ん中
            else if (b == 1) anime[Chain] = 1 + (8 * 3) + (x * 100000) + (y * 1000);//上から右
            else if (a == -1) anime[Chain] = 4 + (8 * 3) + (x * 100000) + (y * 1000);//右から上
            BS[x, y] = -BS[x, y];
            hono = 1;
            Chain++;
            if (y > 0)
            {
                if (BS[x, y - 1] > 0)
                {
                    fire(BS[x, y - 1], x, y - 1, 0, -1);
                }
            }
            if (BS[x + 1, y] > 0)
            {
                fire(BS[x + 1, y], x + 1, y, 1, 0);
            }
        }
        return hono;
    }
    static int simi(int x, int y, int a, int b)
    {
        int hono = 0;
        if (a == -1 || b == -1)
        {
            if (a == b) anime[Chain] = 7 + (8 * 4) + (x * 100000) + (y * 1000);//真ん中
            else if (b == -1) anime[Chain] = 1 + (8 * 4) + (x * 100000) + (y * 1000);//下から右
            else if (a == -1) anime[Chain] = 4 + (8 * 4) + (x * 100000) + (y * 1000);//右から下
            BS[x, y] = -BS[x, y];
            hono = 1;
            Chain++;
            if (BS[x, y + 1] > 0)
            {
                fire(BS[x, y + 1], x, y + 1, 0, 1);
            }
            if (BS[x + 1, y] > 0)
            {
                fire(BS[x + 1, y], x + 1, y, 1, 0);
            }
        }
        return hono;
    }
    static int sihi(int x, int y, int a, int b)
    {
        int hono = 0;
        if (a == 1 || b == -1)
        {
            if (a == b) anime[Chain] = 7 + (8 * 5) + (x * 100000) + (y * 1000);//真ん中
            else if (b == -1) anime[Chain] = 1 + (8 * 5) + (x * 100000) + (y * 1000);//下から左
            else if (a == 1) anime[Chain] = 4 + (8 * 5) + (x * 100000) + (y * 1000);//左から下
            BS[x, y] = -BS[x, y];
            hono = 1;
            Chain++;
            if (BS[x, y + 1] > 0)
            {
                fire(BS[x, y + 1], x, y + 1, 0, 1);
            }
            if (x > 0)
            {
                if (BS[x - 1, y] > 0)
                {
                    fire(BS[x - 1, y], x - 1, y, -1, 0);
                }
            }
        }
        return hono;
    }
    static int fipa(int x, int y, int a, int b)
    {
        /* if (a == b) anime[Chain] = 13 + (8 * 6) + (x * 100000) + (y * 1000);//真ん中
         else if (a == -1) anime[Chain] = 1 + (8 * 6) + (x * 100000) + (y * 1000);//右から
         else if (b == 1) anime[Chain] = 4 + (8 * 6) + (x * 100000) + (y * 1000);//上から
         else if (a == 1) anime[Chain] = 7 + (8 * 6) + (x * 100000) + (y * 1000);//左から
         else if (b == -1) anime[Chain] = 10 + (8 * 6) + (x * 100000) + (y * 1000);//下から*/
        BS[x, y] = -BS[x, y];
        anime[Chain] = -(x * 100000) - (y * 1000);
        Chain++;
        fire(BS[x + 1, y], x + 1, y, 1, 0);
        if (x > 0)
        {
            fire(BS[x - 1, y], x - 1, y, -1, 0);
        }

        fire(BS[x, y + 1], x, y + 1, 0, 1);
        if (y > 0)
        {
            fire(BS[x, y - 1], x, y - 1, 0, -1);
        }
        return 1;
    }
    static int yuki(int x, int y, int a, int b)
    {
        BS[x, y] = -BS[x, y];
        anime[Chain] = -(x * 100000) - (y * 1000);
        Chain++;
        Power++;
        return 1;
    }
    static int bom(int x, int y, int a, int b)
    {
        BS[x, y] = -BS[x, y];
        anime[Chain] = -(x * 100000) - (y * 1000);
        Chain++;
        fire(BS[x + 1, y], x + 1, y, 1, 1);
        fire(BS[x + 1, y], x + 1, y, -1, -1);
        if (x > 0)
        {
            fire(BS[x - 1, y], x - 1, y, 1, 1);
            fire(BS[x - 1, y], x - 1, y, -1, -1);
        }

        fire(BS[x, y + 1], x, y + 1, 1, 1);
        fire(BS[x, y + 1], x, y + 1, -1, -1);
        if (y > 0)
        {
            fire(BS[x, y - 1], x, y - 1, 1, 1);
            fire(BS[x, y - 1], x, y - 1, -1, -1);
        }
        return 1;
    }
    static void Down()
    {

        for (int b = 13; b >= 1; b = b - 1)
        {
            for (int a = 7; a >= 0; a = a - 1)
            {
                if (BS[a, b] == 0)
                {
                    if (BS[a, b - 1] > 0)
                    {
                        BS[a, b] = BS[a, b - 1];
                        BS[a, b - 1] = 0;
                    }
                }
            }
        }


        for (int b = 13; b >= 1; b = b - 1)
        {
            for (int a = 7; a >= 0; a = a - 1)
            {
                if (BS[a, b] == 0)
                {
                    if (BS[a, b - 1] > 0)
                    {
                        Down();
                    }
                }
            }
        }

    }
}
