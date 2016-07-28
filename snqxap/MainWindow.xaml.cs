﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


/*
 * snqxap 增加一战吃多少资源的计算
 * snqxap 增加技能发动率、技能固定伤害、技能说明和其他技能（夜用、力场盾） 烟雾弹算减少命中
 * snqxap 加上步枪技能概率提升
 * snqxap 增加slider 一轮扫射结束  一轮上弹结束 二轮扫射结束 二轮上弹结束 三轮扫射结束
 * snqxap 机枪增加 一轮扫射时间 一轮总时间
 * snqxap mg技能设为在slider一轮。
 * 
 * 
 * 
 * skillcircle:
 * skilltime:
 * skillrate:
 * skilldamage:
 * skillcontent:
 */

namespace snqxap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        Gun[] gun = new Gun[102];
        GunGrid[] gg = new GunGrid[9];
        Double[] skillupdamage = new Double[9];
        Double[] skilluphit = new Double[9];
        Double[] skillupshotspeed = new Double[9];
        Double[] skillupdodge = new Double[9];
        double skilldowndodge;
        double skilldownhit;
        int[] lastgunindex = new int[9];
        int howmany;

        public MainWindow()
        {
       
            InitializeComponent();

            baka();

      //      Lskillread0.Content = gun[45].skillcontent;

        }

        public void baka() {

            howmany = 0;

            for (int i = 0; i < 102; i++)
            { gun[i] = new Gun();
                gun[i].index = 0.00;
            }

            gun[0].name = "汤姆森"; gun[0].what = 3; gun[0].hp = 238; gun[0].damage = 31; gun[0].hit = 12; gun[0].dodge = 54; gun[0].shotspeed = 81; gun[0].crit = 0.05; gun[0].belt = 0; gun[0].number = 2; gun[0].effect0 = 1; gun[0].effect1 = 7; gun[0].damageup = 0.12; gun[0].hitup = 0; gun[0].shotspeedup = 0; gun[0].critup = 0; gun[0].dodgeup = 0.15; gun[0].to = 2;
            gun[0].skilltype = 10; gun[0].skillcircle = 0; gun[0].skilldamage = 0; gun[0].skillrate = 0.4; gun[0].skilltime = 4; gun[0].skillcontent = "力场盾";
            gun[1].name = "司登MkⅡ"; gun[1].what = 3; gun[1].hp = 185; gun[1].damage = 26; gun[1].hit = 15; gun[1].dodge = 75; gun[1].shotspeed = 78; gun[1].crit = 0.05; gun[1].belt = 0; gun[1].number = 3; gun[1].effect0 = 1; gun[1].effect1 = 4; gun[1].effect2 = 7; gun[1].damageup = 0; gun[1].hitup = 0.1; gun[1].shotspeedup = 0; gun[1].critup = 0; gun[1].dodgeup = 0.3; gun[1].to = 2;
            gun[1].skilltype = 15; gun[1].skillcircle = 2.5; gun[1].skilldamage = gun[1].damage * 4; gun[1].skillrate = 0.5; gun[1].skilltime = 0; gun[1].skillcontent = "手榴弹,半径" + gun[1].skillcircle.ToString();
            gun[2].name = "UMP9"; gun[2].what = 3; gun[2].hp = 176; gun[2].damage = 26; gun[2].hit = 14; gun[2].dodge = 76; gun[2].shotspeed = 87; gun[2].crit = 0.05; gun[2].belt = 0; gun[2].number = 3; gun[2].effect0 = 1; gun[2].effect1 = 4; gun[2].effect2 = 7; gun[2].damageup = 0; gun[2].hitup = 0.3; gun[2].shotspeedup = 0.12; gun[2].critup = 0; gun[2].dodgeup = 0; gun[2].to = 2;
            gun[2].skilltype = 11; gun[2].skillcircle = 2.5; gun[2].skilldamage = 0; gun[2].skillrate = 0.6; gun[2].skilltime = 3.6; gun[2].skillcontent = "闪光弹,半径" + gun[2].skillcircle.ToString();
            gun[3].name = "vector"; gun[3].what = 3; gun[3].hp = 185; gun[3].damage = 30; gun[3].hit = 11; gun[3].dodge = 71; gun[3].shotspeed = 101; gun[3].crit = 0.05; gun[3].belt = 0; gun[3].number = 1; gun[3].effect0 = 4; gun[3].damageup = 0; gun[3].hitup = 0; gun[3].shotspeedup = 0.25; gun[3].critup = 0; gun[3].dodgeup = 0; gun[3].to = 2;
            gun[3].skilltype = 12; gun[3].skillcircle = 1; gun[3].skilldamage = gun[3].damage * 3.2; gun[3].skillrate = 0.6; gun[3].skilltime = 4; gun[3].skillcontent = "燃烧弹,半径" + gun[3].skillcircle.ToString() + "不算DOT";
            gun[4].name = "蝎式"; gun[4].what = 3; gun[4].hp = 159; gun[4].damage = 24; gun[4].hit = 13; gun[4].dodge = 83; gun[4].shotspeed = 95; gun[4].crit = 0.05; gun[4].belt = 0; gun[4].number = 1; gun[4].effect0 = 4; gun[4].damageup = 0; gun[4].hitup = 0.5; gun[4].shotspeedup = 0.15; gun[4].critup = 0; gun[4].dodgeup = 0; gun[4].to = 2;
            gun[4].skilltype = 12; gun[4].skillcircle = 1; gun[4].skilldamage = gun[4].damage * 3; gun[4].skillrate = 0.56; gun[4].skilltime = 4; gun[4].skillcontent = "燃烧弹,半径" + gun[4].skillcircle.ToString() + "不算DOT";
            gun[5].name = "M3"; gun[5].what = 3; gun[5].hp = 185; gun[5].damage = 29; gun[5].hit = 13; gun[5].dodge = 67; gun[5].shotspeed = 68; gun[5].crit = 0.05; gun[5].belt = 0; gun[5].number = 1; gun[5].effect0 = 4; gun[5].damageup = 0; gun[5].hitup = 0.4; gun[5].shotspeedup = 0; gun[5].critup = 0; gun[5].dodgeup = 0.3; gun[5].to = 2;
            gun[5].skilltype = 15; gun[5].skillcircle = 2.5; gun[5].skilldamage = gun[5].damage * 4; gun[5].skillrate = 0.45; gun[5].skilltime = 0; gun[5].skillcontent = "手榴弹,半径" + gun[5].skillcircle.ToString();
            gun[6].name = "IDW"; gun[6].what = 3; gun[6].hp = 150; gun[6].damage = 26; gun[6].hit = 15; gun[6].dodge = 85; gun[6].shotspeed = 75; gun[6].crit = 0.05; gun[6].belt = 0; gun[6].number = 3; gun[6].effect0 = 1; gun[6].effect1 = 4; gun[6].effect2 = 7; gun[6].damageup = 0; gun[6].hitup = 0; gun[6].shotspeedup = 0; gun[6].critup = 0; gun[6].dodgeup = 0.2; gun[6].to = 2;
            gun[6].skilltype = 2; gun[6].skillrate = 0.6; gun[6].skilltime = 4.5; gun[6].skillcontent = "降低移速90%,不算";
            gun[7].name = "微型乌兹"; gun[7].what = 3; gun[7].hp = 159; gun[7].damage = 24; gun[7].hit = 11; gun[7].dodge = 79; gun[7].shotspeed = 104; gun[7].crit = 0.05; gun[7].belt = 0; gun[7].number = 2; gun[7].effect0 = 2; gun[7].effect1 = 8; gun[7].damageup = 0.18; gun[7].hitup = 0; gun[7].shotspeedup = 0; gun[7].critup = 0; gun[7].dodgeup = 0; gun[7].to = 2;
            gun[7].skilltype = 12; gun[7].skillcircle = 1; gun[7].skilldamage = gun[7].damage * 3; gun[7].skillrate = 0.56; gun[7].skilltime = 4; gun[7].skillcontent = "燃烧弹,半径" + gun[7].skillcircle.ToString() + "不算DOT"; 
            gun[8].name = "FMG-9"; gun[8].what = 3; gun[8].hp = 141; gun[8].damage = 26; gun[8].hit = 13; gun[8].dodge = 90; gun[8].shotspeed = 92; gun[8].crit = 0.05; gun[8].belt = 0; gun[8].number = 2; gun[8].effect0 = 1; gun[8].effect1 = 7; gun[8].damageup = 0.1; gun[8].hitup = 0; gun[8].shotspeedup = 0; gun[8].critup = 0; gun[8].dodgeup = 0.12; gun[8].to = 2;
            gun[8].skilltype = 23;gun[8].skillrate = 0.4;gun[8].skilltime = 5.4;gun[8].skillupmydodge =1 + 3.6;gun[8].skillcontent = "提升自身闪避360%";
            gun[9].name = "MAC-10"; gun[9].what = 3; gun[9].hp = 176; gun[9].damage = 28; gun[9].hit = 11; gun[9].dodge = 68; gun[9].shotspeed = 91; gun[9].crit = 0.05; gun[9].belt = 0; gun[9].number = 3; gun[9].effect0 = 1; gun[9].effect1 = 4; gun[9].effect2 = 7; gun[9].damageup = 0.12; gun[9].hitup = 0; gun[9].shotspeedup = 0; gun[9].critup = 0; gun[9].dodgeup = 0; gun[9].to = 2;
            gun[9].skilltype = 19;gun[9].skillrate = 0.64; gun[9].skillcircle = 2.5; gun[9].skilltime = 4; gun[9].skilldownallenemyhit = 0.48; gun[9].skillcontent = "降半径" + gun[9].skillcircle.ToString() + "命中48%移速32%";
            gun[10].name = "M45"; gun[10].what = 3; gun[10].hp = 185; gun[10].damage = 30; gun[10].hit = 12; gun[10].dodge = 62; gun[10].shotspeed = 74; gun[10].crit = 0.05; gun[10].belt = 0; gun[10].number = 2; gun[10].effect0 = 1; gun[10].effect1 = 7; gun[10].damageup = 0; gun[10].hitup = 0; gun[10].shotspeedup = 0.1; gun[10].critup = 0; gun[10].dodgeup = 0.1; gun[10].to = 2;
            gun[10].skilltype = 19;gun[10].skillrate = 0.56; gun[10].skillcircle = 2.5; gun[10].skilltime = 4; gun[10].skilldownallenemyhit = 0.48; gun[10].skillcontent = "降半径" + gun[10].skillcircle.ToString() + "命中48%移速32%";
            gun[11].name = "Spectre M4"; gun[11].what = 3; gun[11].hp = 176; gun[11].damage = 25; gun[11].hit = 12; gun[11].dodge = 66; gun[11].shotspeed = 88; gun[11].crit = 0.05; gun[11].belt = 0; gun[11].number = 1; gun[11].effect0 = 4; gun[11].damageup = 0.2; gun[11].hitup = 0; gun[11].shotspeedup = 0; gun[11].critup = 0; gun[11].dodgeup = 0; gun[11].to = 2;
            gun[11].skilltype = 23; gun[11].skillrate = 0.36; gun[11].skilltime = 5.4; gun[11].skillupmydodge = 1 + 3.6; gun[11].skillcontent = "提升自身闪避360%";
            gun[12].name = "PPS-43"; gun[12].what = 3; gun[12].hp = 176; gun[12].damage = 33; gun[12].hit = 13; gun[12].dodge = 65; gun[12].shotspeed = 74; gun[12].crit = 0.05; gun[12].belt = 0; gun[12].number = 3; gun[12].effect0 = 1; gun[12].effect1 = 4; gun[12].effect2 = 7; gun[12].damageup = 0.12; gun[12].hitup = 0; gun[12].shotspeedup = 0; gun[12].critup = 0; gun[12].dodgeup = 0; gun[12].to = 2;
            gun[12].skilltype = 15; gun[12].skillcircle = 2.5; gun[12].skilldamage = gun[12].damage * 4; gun[12].skillrate = 0.5; gun[12].skilltime = 0; gun[12].skillcontent = "手榴弹,半径" + gun[12].skillcircle.ToString();
            gun[13].name = "PP-2000"; gun[13].what = 3; gun[13].hp = 159; gun[13].damage = 29; gun[13].hit = 11; gun[13].dodge = 74; gun[13].shotspeed = 80; gun[13].crit = 0.05; gun[13].belt = 0; gun[13].number = 2; gun[13].effect0 = 1; gun[13].effect1 = 7; gun[13].damageup = 0.1; gun[13].hitup = 0.25; gun[13].shotspeedup = 0; gun[13].critup = 0; gun[13].dodgeup = 0; gun[13].to = 2;
            gun[13].skilltype = 15; gun[13].skillcircle = 2.5; gun[13].skilldamage = gun[13].damage * 4; gun[13].skillrate = 0.45; gun[13].skilltime = 0; gun[13].skillcontent = "手榴弹,半径" + gun[13].skillcircle.ToString();
            gun[14].name = "MP5"; gun[14].what = 3; gun[14].hp = 168; gun[14].damage = 30; gun[14].hit = 13; gun[14].dodge = 68; gun[14].shotspeed = 89; gun[14].crit = 0.05; gun[14].belt = 0; gun[14].number = 2; gun[14].effect0 = 1; gun[14].effect1 = 7; gun[14].damageup = 0; gun[14].hitup = 0; gun[14].shotspeedup = 0; gun[14].critup = 0.2; gun[14].dodgeup = 0.4; gun[14].to = 2;
            gun[14].skilltype = 10; gun[14].skillcircle = 0; gun[14].skilldamage = 0; gun[14].skillrate = 0.48; gun[14].skilltime = 3; gun[14].skillcontent = "力场盾";
            gun[15].name = "伯莱塔38型"; gun[15].what = 3; gun[15].hp = 203; gun[15].damage = 32; gun[15].hit = 10; gun[15].dodge = 52; gun[15].shotspeed = 75; gun[15].crit = 0.05; gun[15].belt = 0; gun[15].number = 2; gun[15].effect0 = 1; gun[15].effect1 = 7; gun[15].damageup = 0.05; gun[15].hitup = 0; gun[15].shotspeedup = 0.1; gun[15].critup = 0; gun[15].dodgeup = 0; gun[15].to = 2;
            gun[15].skilltype = 11; gun[15].skillcircle = 2.5; gun[15].skilldamage = 0; gun[15].skillrate = 0.6; gun[15].skilltime = 3; gun[15].skillcontent = "闪光弹,半径" + gun[15].skillcircle.ToString();
            gun[16].name = "MP40"; gun[16].what = 3; gun[16].hp = 185; gun[16].damage = 29; gun[16].hit = 13; gun[16].dodge = 58; gun[16].shotspeed = 76; gun[16].crit = 0.05; gun[16].belt = 0; gun[16].number = 2; gun[16].effect0 = 1; gun[16].effect1 = 7; gun[16].damageup = 0; gun[16].hitup = 0.25; gun[16].shotspeedup = 0; gun[16].critup = 0; gun[16].dodgeup = 0.2; gun[16].to = 2;
            gun[16].skilltype = 12; gun[16].skillcircle = 1; gun[16].skilldamage = gun[16].damage * 3; gun[16].skillrate = 0.5; gun[16].skilltime = 4; gun[16].skillcontent = "燃烧弹,半径" + gun[16].skillcircle.ToString() + "不算DOT";
            gun[17].name = "PPSh-41"; gun[17].what = 3; gun[17].hp = 185; gun[17].damage = 26; gun[17].hit = 11; gun[17].dodge = 56; gun[17].shotspeed = 93; gun[17].crit = 0.05; gun[17].belt = 0; gun[17].number = 2; gun[17].effect0 = 2; gun[17].effect1 = 8; gun[17].damageup = 0.1; gun[17].hitup = 0; gun[17].shotspeedup = 0.05; gun[17].critup = 0; gun[17].dodgeup = 0; gun[17].to = 2;
            gun[17].skilltype = 15; gun[17].skillcircle = 2.5; gun[17].skilldamage = gun[17].damage * 4; gun[17].skillrate = 0.45; gun[17].skilltime = 0; gun[17].skillcontent = "手榴弹,半径" + gun[17].skillcircle.ToString();
            gun[18].name = "64式"; gun[18].what = 3; gun[18].hp = 176; gun[18].damage = 27; gun[18].hit = 11; gun[18].dodge = 59; gun[18].shotspeed = 93; gun[18].crit = 0.05; gun[18].belt = 0; gun[18].number = 1; gun[18].effect0 = 4; gun[18].damageup = 0; gun[18].hitup = 0; gun[18].shotspeedup = 0.2; gun[18].critup = 0; gun[18].dodgeup = 0; gun[18].to = 2;
            gun[18].skilltype = 11; gun[18].skillcircle = 2.5; gun[18].skilldamage = 0; gun[18].skillrate = 0.6; gun[18].skilltime = 3; gun[18].skillcontent = "闪光弹,半径" + gun[18].skillcircle.ToString();
            gun[19].name = "UMP45"; gun[19].what = 3; gun[19].hp = 185; gun[19].damage = 27; gun[19].hit = 13; gun[19].dodge = 73; gun[19].shotspeed = 82; gun[19].crit = 0.05; gun[19].belt = 0; gun[19].number = 2; gun[19].effect0 = 1; gun[19].effect1 = 7; gun[19].damageup = 0.15; gun[19].hitup = 0.0; gun[19].shotspeedup = 0; gun[19].critup = 0.5; gun[19].dodgeup = 0; gun[19].to = 2;
            gun[19].skilltype = 19; gun[19].skillrate = 0.56; gun[19].skillcircle = 2.5; gun[19].skilltime = 4; gun[19].skilldownallenemyhit = 0.48; gun[19].skillcontent = "降半径" + gun[19].skillcircle.ToString() + "命中48%移速40%";
            gun[20].name = "索米"; gun[20].what = 3; gun[20].hp = 220; gun[20].damage = 28; gun[20].hit = 15; gun[20].dodge = 56; gun[20].shotspeed = 92; gun[20].crit = 0.05; gun[20].belt = 0; gun[20].number = 3; gun[20].effect0 = 1; gun[20].effect1 = 4; gun[20].effect2 = 7; gun[20].damageup = 0; gun[20].hitup = 0.3; gun[20].shotspeedup = 0.15; gun[20].critup = 0; gun[20].dodgeup = 0; gun[20].to = 2;
            gun[20].skilltype = 23; gun[20].skillrate = 0.45; gun[20].skilltime = 5.4; gun[20].skillupmydodge = 1 + 3.6; gun[20].skillcontent = "提升自身闪避360%";
            gun[21].name = "OTs-12"; gun[21].what = 2; gun[21].hp = 105; gun[21].damage = 42; gun[21].hit = 54; gun[21].dodge = 54; gun[21].shotspeed = 72; gun[21].crit = 0.2; gun[21].belt = 0; gun[21].number = 2; gun[21].effect0 = 3; gun[21].effect1 = 6; gun[21].damageup = 0.15; gun[21].hitup = 0; gun[21].shotspeedup = 0.2; gun[21].critup = 0; gun[21].dodgeup = 0; gun[21].to = 3;
            gun[21].skilltype = 18;gun[21].skillrate = 0.64;gun[21].skilltime = 8;gun[21].skillupmyshotspeed = 1 + 0.64;gun[21].skillcontent = "提升自身射速64%";
            gun[22].name = "G36"; gun[22].what = 2; gun[22].hp = 127; gun[22].damage = 47; gun[22].hit = 44; gun[22].dodge = 41; gun[22].shotspeed = 72; gun[22].crit = 0.2; gun[22].belt = 0; gun[22].number = 2; gun[22].effect0 = 6; gun[22].effect1 = 9; gun[22].damageup = 0.3; gun[22].hitup = 0; gun[22].shotspeedup = 0.15; gun[22].critup = 0; gun[22].dodgeup = 0; gun[22].to = 3;
            gun[22].skilltype = 6;gun[22].skillrate = 0.38;gun[22].skilltime = 9.6;gun[22].skillupmydamage = 1 + 0.96;gun[22].skillcontent = "提升自身伤害96%";
            gun[23].name = "FAL"; gun[23].what = 2; gun[23].hp = 132; gun[23].damage = 57; gun[23].hit = 40; gun[23].dodge = 38; gun[23].shotspeed = 70; gun[23].crit = 0.2; gun[23].belt = 0; gun[23].number = 3; gun[23].effect0 = 3; gun[23].effect1 = 6; gun[23].effect2 = 9; gun[23].damageup = 0; gun[23].hitup = 0; gun[23].shotspeedup = 0; gun[23].critup = 0; gun[23].dodgeup = 0.2; gun[23].to = 3;
            gun[23].skilltype = 1;gun[23].skilldamage = gun[23].damage * 2.5;gun[23].skillcircle = 4;gun[23].skillrate = 0.45;gun[23].skillcontent = "爆破榴弹,半径" + gun[23].skillcircle.ToString();
            gun[24].name = "HK416"; gun[24].what = 2; gun[24].hp = 121; gun[24].damage = 51; gun[24].hit = 46; gun[24].dodge = 43; gun[24].shotspeed = 76; gun[24].crit = 0.2; gun[24].belt = 0; gun[24].number = 1; gun[24].effect0 = 6; gun[24].damageup = 0.4; gun[24].hitup = 0; gun[24].shotspeedup = 0; gun[24].critup = 0; gun[24].dodgeup = 0; gun[24].to = 3;
            gun[24].skilltype = 1; gun[24].skilldamage = gun[24].damage * 6; gun[24].skillcircle = 1; gun[24].skillrate = 0.6; gun[24].skillcontent = "杀伤榴弹,半径" + gun[24].skillcircle.ToString();
            gun[25].name = "G41"; gun[25].what = 2; gun[25].hp = 127; gun[25].damage = 50; gun[25].hit = 48; gun[25].dodge = 40; gun[25].shotspeed = 77; gun[25].crit = 0.2; gun[25].belt = 0; gun[25].number = 2; gun[25].effect0 = 3; gun[25].effect1 = 9; gun[25].damageup = 0; gun[25].hitup = 0.5; gun[25].shotspeedup = 0; gun[25].critup = 0; gun[25].dodgeup = 0.15; gun[25].to = 3;
            gun[25].skilltype = 6; gun[25].skillrate = 0.38; gun[25].skilltime = 9.6; gun[25].skillupmydamage = 1 + 1.04; gun[25].skillcontent = "提升自身伤害104%";
            gun[26].name = "56-1式"; gun[26].what = 2; gun[26].hp = 138; gun[26].damage = 52; gun[26].hit = 35; gun[26].dodge = 35; gun[26].shotspeed = 69; gun[26].crit = 0.2; gun[26].belt = 0; gun[26].number = 1; gun[26].effect0 = 6; gun[26].damageup = 0; gun[26].hitup = 0; gun[26].shotspeedup = 0; gun[26].critup = 0.1; gun[26].dodgeup = 0.15; gun[26].to = 3;
            gun[26].skilltype = 1; gun[26].skilldamage = gun[26].damage * 2; gun[26].skillcircle = 4; gun[26].skillrate = 0.5; gun[26].skillcontent = "爆破榴弹,半径" + gun[26].skillcircle.ToString();
            gun[27].name = "M4A1"; gun[27].what = 2; gun[27].hp = 110; gun[27].damage = 46; gun[27].hit = 48; gun[27].dodge = 48; gun[27].shotspeed = 79; gun[27].crit = 0.2; gun[27].belt = 0; gun[27].number = 1; gun[27].effect0 = 7; gun[27].damageup = 0.18; gun[27].hitup = 0; gun[27].shotspeedup = 0; gun[27].critup = 0; gun[27].dodgeup = 0; gun[27].to = 2;
            gun[27].skilltype = 20;gun[27].skillrate = 0.58;gun[27].skilltime = 12.8; gun[27].skilldownonedodge =0.96;gun[27].skillcontent = "降低当前目标闪避96%,不算";
            gun[28].name = "M16A1"; gun[28].what = 2; gun[28].hp = 116; gun[28].damage = 47; gun[28].hit = 46; gun[28].dodge = 44; gun[28].shotspeed = 75; gun[28].crit = 0.2; gun[28].belt = 0; gun[28].number = 1; gun[28].effect0 = 1; gun[28].damageup = 0.18; gun[28].hitup = 0; gun[28].shotspeedup = 0; gun[28].critup = 0; gun[28].dodgeup = 0; gun[28].to = 2;
            gun[28].skilltype = 11; gun[28].skillcircle = 2.5; gun[28].skilldamage = 0; gun[28].skillrate = 0.6; gun[28].skilltime = 3.6; gun[28].skillcontent = "闪光弹,半径" + gun[28].skillcircle.ToString();
            gun[29].name = "ST AR-15"; gun[29].what = 2; gun[29].hp = 105; gun[29].damage = 48; gun[29].hit = 50; gun[29].dodge = 50; gun[29].shotspeed = 77; gun[29].crit = 0.2; gun[29].belt = 0; gun[29].number = 1; gun[29].effect0 = 9; gun[29].damageup = 0; gun[29].hitup = 0; gun[29].shotspeedup = 0; gun[29].critup = 0; gun[29].dodgeup = 0.36; gun[29].to = 2;
            gun[29].skilltype = 18; gun[29].skillrate = 0.64; gun[29].skilltime = 9.6; gun[29].skillupmyshotspeed = 1 + 0.576; gun[29].skillcontent = "提升自身射速57.6%";
            gun[30].name = "FAMAS"; gun[30].what = 2; gun[30].hp = 121; gun[30].damage = 44; gun[30].hit = 48; gun[30].dodge = 40; gun[30].shotspeed = 81; gun[30].crit = 0.2; gun[30].belt = 0; gun[30].number = 1; gun[30].effect0 = 9; gun[30].damageup = 0.25; gun[30].hitup = 0.6; gun[30].shotspeedup = 0; gun[30].critup = 0; gun[30].dodgeup = 0; gun[30].to = 3;
            gun[30].skilltype = 1; gun[30].skilldamage = gun[30].damage * 2; gun[30].skillcircle = 4; gun[30].skillrate = 0.5; gun[30].skillcontent = "爆破榴弹,半径" + gun[30].skillcircle.ToString();
            gun[31].name = "AK-47"; gun[31].what = 2; gun[31].hp = 132; gun[31].damage = 52; gun[31].hit = 35; gun[31].dodge = 34; gun[31].shotspeed = 65; gun[31].crit = 0.2; gun[31].belt = 0; gun[31].number = 1; gun[31].effect0 = 8; gun[31].damageup = 0; gun[31].hitup = 0; gun[31].shotspeedup = 0; gun[31].critup = 0; gun[31].dodgeup = 0.18; gun[31].to = 3;
            gun[31].skilltype = 3;gun[31].skillrate = 0.64;gun[31].skilltime = 4.5;gun[31].skilldownonedamage = 0.96; gun[31].skillcontent = "降低当前目标96%伤害,不算";
            gun[32].name = "StG44"; gun[32].what = 2; gun[32].hp = 127; gun[32].damage = 53; gun[32].hit = 46; gun[32].dodge = 36; gun[32].shotspeed = 61; gun[32].crit = 0.2; gun[32].belt = 0; gun[32].number = 1; gun[32].effect0 = 6; gun[32].damageup = 0; gun[32].hitup = 0.6; gun[32].shotspeedup = 0; gun[32].critup = 0; gun[32].dodgeup = 0.2; gun[32].to = 3;
            gun[32].skilltype = 1; gun[32].skilldamage = gun[32].damage * 2; gun[32].skillcircle = 4; gun[32].skillrate = 0.45; gun[32].skillcontent = "爆破榴弹,半径" + gun[32].skillcircle.ToString();
            gun[33].name = "CZ-805"; gun[33].what = 2; gun[33].hp = 116; gun[33].damage = 43; gun[33].hit = 49; gun[33].dodge = 41; gun[33].shotspeed = 75; gun[33].crit = 0.2; gun[33].belt = 0; gun[33].number = 2; gun[33].effect0 = 3; gun[33].effect1 = 9; gun[33].damageup = 0; gun[33].hitup = 0.5; gun[33].shotspeedup = 0.25; gun[33].critup = 0; gun[33].dodgeup = 0; gun[33].to = 3;
            gun[33].skilltype = 1; gun[33].skilldamage = gun[33].damage * 2; gun[33].skillcircle = 4; gun[33].skillrate = 0.45; gun[33].skillcontent = "爆破榴弹,半径" + gun[33].skillcircle.ToString();
            gun[34].name = "m4 SOPMPDⅡ"; gun[34].what = 2; gun[34].hp = 110; gun[34].damage = 47; gun[34].hit = 49; gun[34].dodge = 44; gun[34].shotspeed = 78; gun[34].crit = 0.2; gun[34].belt = 0; gun[34].number = 1; gun[34].effect0 = 3; gun[34].damageup = 0; gun[34].hitup = 0; gun[34].shotspeedup = 0; gun[34].critup = 0; gun[34].dodgeup = 0.36; gun[34].to = 2;
            gun[34].skilltype = 1; gun[34].skilldamage = gun[34].damage * 5.5; gun[34].skillcircle = 1; gun[34].skillrate = 0.6; gun[34].skillcontent = "杀伤榴弹,半径" + gun[34].skillcircle.ToString();
            gun[35].name = "TAR-21"; gun[35].what = 2; gun[35].hp = 105; gun[35].damage = 49; gun[35].hit = 48; gun[35].dodge = 44; gun[35].shotspeed = 79; gun[35].crit = 0.2; gun[35].belt = 0; gun[35].number = 2; gun[35].effect0 = 3; gun[35].effect1 = 9; gun[35].damageup = 0; gun[35].hitup = 0; gun[35].shotspeedup = 0; gun[35].critup = 0; gun[35].dodgeup = 0.18; gun[35].to = 3;
            gun[35].skilltype = 18; gun[35].skillrate = 0.88; gun[35].skilltime = 16; gun[35].skillupmyshotspeed = 1 + 0.512; gun[35].skillcontent = "(夜)提升自身射速51.2%";
            gun[36].name = "加利尔"; gun[36].what = 2; gun[36].hp = 105; gun[36].damage = 50; gun[36].hit = 44; gun[36].dodge = 43; gun[36].shotspeed = 66; gun[36].crit = 0.2; gun[36].belt = 0; gun[36].number = 1; gun[36].effect0 = 6; gun[36].damageup = 0; gun[36].hitup = 0.5; gun[36].shotspeedup = 0; gun[36].critup = 0; gun[36].dodgeup = 0.1; gun[36].to = 3;
            gun[36].skilltype = 9;gun[36].skilltime = 17.6;gun[36].skillrate = 0.88;gun[36].skillupmyhit = 1 + 4.4;gun[36].skillcontent = "(夜)提升自身命中440%";
            gun[37].name = "SIG-510"; gun[37].what = 2; gun[37].hp = 116; gun[37].damage = 54; gun[37].hit = 41; gun[37].dodge = 37; gun[37].shotspeed = 59; gun[37].crit = 0.2; gun[37].belt = 0; gun[37].number = 2; gun[37].effect0 = 3; gun[37].effect1 = 9; gun[37].damageup = 0.2; gun[37].hitup = 0; gun[37].shotspeedup = 0.1; gun[37].critup = 0; gun[37].dodgeup = 0; gun[37].to = 3;
            gun[37].skilltype = 24;gun[37].skilltime = 14.4;gun[37].skillrate = 0.72;gun[37].skillupallhit = 0.9;gun[37].skillcontent = "(夜)提升己方命中90%";
            gun[38].name = "G3"; gun[38].what = 2; gun[38].hp = 110; gun[38].damage = 55; gun[38].hit = 46; gun[38].dodge = 38; gun[38].shotspeed = 61; gun[38].crit = 0.2; gun[38].belt = 0; gun[38].number = 1; gun[38].effect0 = 2; gun[38].damageup = 0; gun[38].hitup = 0.5; gun[38].shotspeedup = 0.2; gun[38].critup = 0; gun[38].dodgeup = 0; gun[38].to = 3;
            gun[38].skilltype = 1; gun[38].skilldamage = gun[38].damage * 5.5; gun[38].skillcircle = 1; gun[38].skillrate = 0.5; gun[38].skillcontent = "杀伤榴弹,半径" + gun[38].skillcircle.ToString();
            gun[39].name = "F2000"; gun[39].what = 2; gun[39].hp = 105; gun[39].damage = 42; gun[39].hit = 44; gun[39].dodge = 40; gun[39].shotspeed = 81; gun[39].crit = 0.2; gun[39].belt = 0; gun[39].number = 1; gun[39].effect0 = 6; gun[39].damageup = 0.2; gun[39].hitup = 0; gun[39].shotspeedup = 0; gun[39].critup = 0; gun[39].dodgeup = 0.1; gun[39].to = 3;
            gun[39].skilltype = 6; gun[39].skillrate = 0.58; gun[39].skilltime = 3.2; gun[39].skillupmydamage = 1 + 1.6; gun[39].skillcontent = "提升自身伤害160%";
            gun[40].name = "FNC"; gun[40].what = 2; gun[40].hp = 110; gun[40].damage = 51; gun[40].hit = 46; gun[40].dodge = 37; gun[40].shotspeed = 73; gun[40].crit = 0.2; gun[40].belt = 0; gun[40].number = 1; gun[40].effect0 = 3; gun[40].damageup = 0; gun[40].hitup = 0.5; gun[40].shotspeedup = 0; gun[40].critup = 0; gun[40].dodgeup = 0.12; gun[40].to = 3;
            gun[40].skilltype = 6; gun[40].skillrate = 0.64; gun[40].skilltime = 3.2; gun[40].skillupmydamage = 1 + 1.6; gun[40].skillcontent = "提升自身伤害160%";
            gun[41].name = "L85A1"; gun[41].what = 2; gun[41].hp = 94; gun[41].damage = 46; gun[41].hit = 43; gun[41].dodge = 43; gun[41].shotspeed = 78; gun[41].crit = 0.2; gun[41].belt = 0; gun[41].number = 1; gun[41].effect0 = 2; gun[41].damageup = 0.2; gun[41].hitup = 0.5; gun[41].shotspeedup = 0; gun[41].critup = 0; gun[41].dodgeup = 0; gun[41].to = 3;
            gun[41].skilltype = 16;gun[41].skillrate = 0.64;gun[41].skilltime = 4;gun[41].skilldownoneshotspeed = 0.96; gun[41].skillcontent = "降低当前目标96%射速,不算";
            gun[42].name = "9a91"; gun[42].what = 2; gun[42].hp = 116; gun[42].damage = 41; gun[42].hit = 48; gun[42].dodge = 50; gun[42].shotspeed = 78; gun[42].crit = 0.2; gun[42].belt = 0; gun[42].number = 2; gun[42].effect0 = 3; gun[42].effect1 = 9; gun[42].damageup = 0; gun[42].hitup = 0; gun[42].shotspeedup = 0.1; gun[42].critup = 0; gun[42].dodgeup = 0.15; gun[42].to = 3;
            gun[42].skilltype = 6; gun[42].skillrate = 0.576; gun[42].skilltime = 8; gun[42].skillupmydamage = 1 + 1.6; gun[42].skillcontent = "(夜)提升自身伤害160%";
            gun[43].name = "AS Val"; gun[43].what = 2; gun[43].hp = 132; gun[43].damage = 39; gun[43].hit = 46; gun[43].dodge = 49; gun[43].shotspeed = 75; gun[43].crit = 0.2; gun[43].belt = 0; gun[43].number = 1; gun[43].effect0 = 2; gun[43].damageup = 0.25; gun[43].hitup = 0; gun[43].shotspeedup = 0.1; gun[43].critup = 0; gun[43].dodgeup = 0; gun[43].to = 3;
            gun[43].skilltype = 1; gun[43].skilldamage = gun[43].damage * 5.5; gun[43].skillcircle = 1; gun[43].skillrate = 0.6; gun[43].skillcontent = "杀伤榴弹,半径" + gun[43].skillcircle.ToString();
            gun[44].name = "维尔德"; gun[44].what = 4; gun[44].hp = 80; gun[44].damage = 27; gun[44].hit = 71; gun[44].dodge = 89; gun[44].shotspeed = 51; gun[44].crit = 0.2; gun[44].belt = 0; gun[44].number = 4; gun[44].effect0 = 1; gun[44].effect1 = 2; gun[44].effect2 = 4; gun[44].effect3 = 7; gun[44].damageup = 0.18; gun[44].hitup = 0; gun[44].shotspeedup = 0.1; gun[44].critup = 0; gun[44].dodgeup = 0; gun[44].to = 1;
            gun[44].skilltype = 8;gun[44].skillrate = 0.6;gun[44].skilltime = 6;gun[44].skilldownallenemyhit = 0.45;gun[44].skillcontent = "降低敌方命中45%";
            gun[45].name = "纳甘左轮"; gun[45].what = 4; gun[45].hp = 70; gun[45].damage = 32; gun[45].hit = 46; gun[45].dodge = 92; gun[45].shotspeed = 44; gun[45].crit = 0.2; gun[45].belt = 0; gun[45].number = 2; gun[45].effect0 = 2; gun[45].effect1 = 8; gun[45].damageup = 0.25; gun[45].hitup = 0; gun[45].shotspeedup = 0; gun[45].critup = 0.1; gun[45].dodgeup = 0; gun[45].to = 1;
            gun[45].skilltype = 5;gun[45].skillrate = 0.64;gun[45].skilltime = 4.8;gun[45].skilldownallenemydamage = 0.48;gun[45].skillcontent = "(夜)降低敌方伤害48%,不算";
            gun[46].name = "柯尔特左轮"; gun[46].what = 4; gun[46].hp = 80; gun[46].damage = 36; gun[46].hit = 49; gun[46].dodge = 76; gun[46].shotspeed = 47; gun[46].crit = 0.2; gun[46].belt = 0; gun[46].number = 4; gun[46].effect0 = 2; gun[46].effect1 = 4; gun[46].effect2 = 6; gun[46].effect3 = 8; gun[46].damageup = 0.15; gun[46].hitup = 0.5; gun[46].shotspeedup = 0; gun[46].critup = 0; gun[46].dodgeup = 0; gun[46].to = 1;
            gun[46].skilltype = 4;gun[46].skillrate = 0.56;gun[46].skilltime = 8;gun[46].skillupalldamage =1 + 0.192;gun[46].skillcontent = "提升己方19.2%伤害";
            gun[47].name = "灰熊MkⅤ"; gun[47].what = 4; gun[47].hp = 86; gun[47].damage = 38; gun[47].hit = 51; gun[47].dodge = 66; gun[47].shotspeed = 54; gun[47].crit = 0.2; gun[47].belt = 0; gun[47].number = 5; gun[47].effect0 = 1; gun[47].effect1 = 2; gun[47].effect2 = 6; gun[47].effect3 = 7; gun[47].effect4 = 8; gun[47].damageup = 0.18; gun[47].hitup = 0; gun[47].shotspeedup = 0; gun[47].critup = 0; gun[47].dodgeup = 0.2; gun[47].to = 1;
            gun[47].skilltype = 4; gun[47].skillrate = 0.56; gun[47].skilltime = 8; gun[47].skillupalldamage = 1 + 0.24; gun[47].skillcontent = "提升己方24%伤害";
            gun[48].name = "托卡列夫"; gun[48].what = 4; gun[48].hp = 86; gun[48].damage = 31; gun[48].hit = 47; gun[48].dodge = 66; gun[48].shotspeed = 52; gun[48].crit = 0.2; gun[48].belt = 0; gun[48].number = 4; gun[48].effect0 = 2; gun[48].effect1 = 3; gun[48].effect2 = 8; gun[48].effect3 = 9; gun[48].damageup = 0; gun[48].hitup = 0.5; gun[48].shotspeedup = 0.12; gun[48].critup = 0; gun[48].dodgeup = 0; gun[48].to = 1;
            gun[48].skilltype = 21;gun[48].skillrate = 0.51;gun[48].skilltime = 8;gun[48].skillupalldodge = 1 + 0.48;gun[48].skillcontent = "提升己方48%闪避";
            gun[49].name = "格洛克17"; gun[49].what = 4; gun[49].hp = 63; gun[49].damage = 29; gun[49].hit = 58; gun[49].dodge = 97; gun[49].shotspeed = 61; gun[49].crit = 0.2; gun[49].belt = 0; gun[49].number = 5; gun[49].effect0 = 1; gun[49].effect1 = 3; gun[49].effect2 = 6; gun[49].effect3 = 7; gun[49].effect4 = 9; gun[49].damageup = 0; gun[49].hitup = 0.5; gun[49].shotspeedup = 0; gun[49].critup = 0; gun[49].dodgeup = 0.25; gun[49].to = 1;
            gun[49].skilltype = 5; gun[49].skillrate = 0.38; gun[49].skilltime = 4.8; gun[49].skilldownallenemydamage = 0.448; gun[49].skillcontent = "降低敌方伤害44.8%,不算";
            gun[50].name = "马卡洛夫"; gun[50].what = 4; gun[50].hp = 63; gun[50].damage = 26; gun[50].hit = 61; gun[50].dodge = 96; gun[50].shotspeed = 61; gun[50].crit = 0.2; gun[50].belt = 0; gun[50].number = 4; gun[50].effect0 = 1; gun[50].effect1 = 4; gun[50].effect2 = 6; gun[50].effect3 = 7; gun[50].damageup = 0.12; gun[50].hitup = 0; gun[50].shotspeedup = 0.1; gun[50].critup = 0; gun[50].dodgeup = 0; gun[50].to = 1;
            gun[50].skilltype = 8; gun[50].skillrate = 0.75; gun[50].skilltime = 6; gun[50].skilldownallenemyhit = 0.6; gun[50].skillcontent = "(夜)降低敌方命中60%";
            gun[51].name = "斯捷奇金"; gun[51].what = 4; gun[51].hp = 83; gun[51].damage = 28; gun[51].hit = 44; gun[51].dodge = 66; gun[51].shotspeed = 65; gun[51].crit = 0.2; gun[51].belt = 0; gun[51].number = 4; gun[51].effect0 = 2; gun[51].effect1 = 3; gun[51].effect2 = 8; gun[51].effect3 = 9; gun[51].damageup = 0.1; gun[51].hitup = 0; gun[51].shotspeedup = 0.15; gun[51].critup = 0; gun[51].dodgeup = 0; gun[51].to = 1;
            gun[51].skilltype = 26;gun[51].skillrate = 0.56;gun[51].skilltime = 16;gun[51].skillupallshotspeed = 1 + 0.096;gun[51].skillcontent = "提升己方9.6%射速";
            gun[52].name = "阿斯特拉左轮"; gun[52].what = 4; gun[52].hp = 80; gun[52].damage = 33; gun[52].hit = 45; gun[52].dodge = 68; gun[52].shotspeed = 52; gun[52].crit = 0.2; gun[52].belt = 0; gun[52].number = 4; gun[52].effect0 = 1; gun[52].effect1 = 3; gun[52].effect2 = 7; gun[52].effect3 = 9; gun[52].damageup = 0; gun[52].hitup = 0; gun[52].shotspeedup = 0.15; gun[52].critup = 0; gun[52].dodgeup = 0.15; gun[52].to = 1;
            gun[52].skilltype = 26; gun[52].skillrate = 0.64; gun[52].skilltime = 8; gun[52].skillupallshotspeed = 1 + 0.16; gun[52].skillcontent = "提升己方16%射速";
            gun[53].name = "P08"; gun[53].what = 4; gun[53].hp = 70; gun[53].damage = 31; gun[53].hit = 46; gun[53].dodge = 80; gun[53].shotspeed = 55; gun[53].crit = 0.2; gun[53].belt = 0; gun[53].number = 2; gun[53].effect0 = 2; gun[53].effect1 = 8; gun[53].damageup = 0.2; gun[53].hitup = 0.6; gun[53].shotspeedup = 0; gun[53].critup = 0; gun[53].dodgeup = 0; gun[53].to = 1;
            gun[53].skilltype = 21; gun[53].skillrate = 0.81; gun[53].skilltime = 14.4; gun[53].skillupalldodge = 1 + 0.72; gun[53].skillcontent = "(夜)提升己方72%闪避";
            gun[54].name = "Mk23"; gun[54].what = 4; gun[54].hp = 80; gun[54].damage = 29; gun[54].hit = 53; gun[54].dodge = 66; gun[54].shotspeed = 63; gun[54].crit = 0.2; gun[54].belt = 0; gun[54].number = 4; gun[54].effect0 = 3; gun[54].effect1 = 4; gun[54].effect2 = 6; gun[54].effect3 = 9; gun[54].damageup = 0.25; gun[54].hitup = 0; gun[54].shotspeedup = 0; gun[54].critup = 0; gun[54].dodgeup = 0; gun[54].to = 1;
            gun[54].skilltype = 4; gun[54].skillrate = 0.56; gun[54].skilltime = 16; gun[54].skillupalldamage = 1 + 0.192; gun[54].skillcontent = "(夜)提升己方19.2%伤害";
            gun[55].name = "M1911"; gun[55].what = 4; gun[55].hp = 73; gun[55].damage = 27; gun[55].hit = 50; gun[55].dodge = 74; gun[55].shotspeed = 57; gun[55].crit = 0.2; gun[55].belt = 0; gun[55].number = 4; gun[55].effect0 = 2; gun[55].effect1 = 4; gun[55].effect2 = 6; gun[55].effect3 = 8; gun[55].damageup = 0; gun[55].hitup = 0.5; gun[55].shotspeedup = 0.1; gun[55].critup = 0; gun[55].dodgeup = 0; gun[55].to = 1;
            gun[55].skilltype = 19; gun[55].skillrate = 0.56; gun[55].skillcircle = 2.5; gun[55].skilltime = 4; gun[55].skilldownallenemyhit = 0.48; gun[55].skillcontent = "降半径" + gun[55].skillcircle.ToString() + "命中48%移速32%";
            gun[56].name = "PPK"; gun[56].what = 4; gun[56].hp = 57; gun[56].damage = 25; gun[56].hit = 59; gun[56].dodge = 100; gun[56].shotspeed = 63; gun[56].crit = 0.2; gun[56].belt = 0; gun[56].number = 4; gun[56].effect0 = 1; gun[56].effect1 = 4; gun[56].effect2 = 6; gun[56].effect3 = 7; gun[56].damageup = 0; gun[56].hitup = 0; gun[56].shotspeedup = 0.2; gun[56].critup = 0.1; gun[56].dodgeup = 0; gun[56].to = 1;
            gun[56].skilltype = 4; gun[56].skillrate = 0.56; gun[56].skilltime = 8; gun[56].skillupalldamage = 1 + 0.16; gun[56].skillcontent = "提升己方16%伤害";
            gun[57].name = "C96"; gun[57].what = 4; gun[57].hp = 83; gun[57].damage = 29; gun[57].hit = 41; gun[57].dodge = 61; gun[57].shotspeed = 62; gun[57].crit = 0.2; gun[57].belt = 0; gun[57].number = 3; gun[57].effect0 = 1; gun[57].effect1 = 6; gun[57].effect2 = 7; gun[57].damageup = 0; gun[57].hitup = 0.5; gun[57].shotspeedup = 0; gun[57].critup = 0; gun[57].dodgeup = 0.25; gun[57].to = 1;
            gun[57].skilltype = 24; gun[57].skilltime = 14.4; gun[57].skillrate = 0.72; gun[57].skillupallhit = 0.99; gun[57].skillcontent = "(夜)提升己方命中99%";
            gun[58].name = "M950A"; gun[58].what = 4; gun[58].hp = 76; gun[58].damage = 30; gun[58].hit = 55; gun[58].dodge = 68; gun[58].shotspeed = 72; gun[58].crit = 0.2; gun[58].belt = 0; gun[58].number = 4; gun[58].effect0 = 1; gun[58].effect1 = 3; gun[58].effect2 = 7; gun[58].effect3 = 9; gun[58].damageup = 0; gun[58].hitup = 0.5; gun[58].shotspeedup = 0.18; gun[58].critup = 0; gun[58].dodgeup = 0; gun[58].to = 1;
            gun[58].skilltype = 26; gun[58].skillrate = 0.56; gun[58].skilltime = 8; gun[58].skillupallshotspeed = 1 + 0.24; gun[58].skillcontent = "提升己方24%射速";
            gun[59].name = "P38"; gun[59].what = 4; gun[59].hp = 66; gun[59].damage = 28; gun[59].hit = 49; gun[59].dodge = 81; gun[59].shotspeed = 57; gun[59].crit = 0.2; gun[59].belt = 0; gun[59].number = 4; gun[59].effect0 = 2; gun[59].effect1 = 3; gun[59].effect2 = 8; gun[59].effect3 = 9; gun[59].damageup = 0; gun[59].hitup = 0.5; gun[59].shotspeedup = 0.1; gun[59].critup = 0; gun[59].dodgeup = 0; gun[59].to = 1;
            gun[59].skilltype = 24; gun[59].skilltime = 14.4; gun[59].skillrate = 0.72; gun[59].skillupallhit = 0.9; gun[59].skillcontent = "(夜)提升己方命中90%";
            gun[60].name = "M9"; gun[60].what = 4; gun[60].hp = 76; gun[60].damage = 29; gun[60].hit = 56; gun[60].dodge = 66; gun[60].shotspeed = 61; gun[60].crit = 0.2; gun[60].belt = 0; gun[60].number = 4; gun[60].effect0 = 1; gun[60].effect1 = 2; gun[60].effect2 = 7; gun[60].effect3 = 8; gun[60].damageup = 0; gun[60].hitup = 0; gun[60].shotspeedup = 0; gun[60].critup = 0; gun[60].dodgeup = 0.4; gun[60].to = 1;
            gun[60].skilltype = 11; gun[60].skillcircle = 2.5; gun[60].skilldamage = 0; gun[60].skillrate = 0.6; gun[60].skilltime = 3.2; gun[60].skillcontent = "闪光弹,半径" + gun[60].skillcircle.ToString();
            gun[61].name = "P7"; gun[61].what = 4; gun[61].hp = 63; gun[61].damage = 32; gun[61].hit = 62; gun[61].dodge = 83; gun[61].shotspeed = 61; gun[61].crit = 0.2; gun[61].belt = 0; gun[61].number = 6; gun[61].effect0 = 1; gun[61].effect1 = 2; gun[61].effect2 = 3; gun[61].effect3 = 7; gun[61].effect4 = 8; gun[61].effect5 = 9; gun[61].damageup = 0; gun[61].hitup = 0; gun[61].shotspeedup = 0.1; gun[61].critup = 0; gun[61].dodgeup = 0.2; gun[61].to = 1;
            gun[61].skilltype = 21; gun[61].skillrate = 0.48; gun[61].skilltime = 16; gun[61].skillupalldodge = 1 + 0.288; gun[61].skillcontent = "提升己方28.8%闪避";
            gun[62].name = "92式"; gun[62].what = 4; gun[62].hp = 63; gun[62].damage = 31; gun[62].hit = 46; gun[62].dodge = 80; gun[62].shotspeed = 61; gun[62].crit = 0.2; gun[62].belt = 0; gun[62].number = 8; gun[62].effect0 = 1; gun[62].effect1 = 2; gun[62].effect2 = 3; gun[62].effect3 = 4; gun[62].effect4 = 6; gun[62].effect5 = 7; gun[62].effect6 = 8; gun[62].effect7 = 9; gun[62].damageup = 0; gun[62].hitup = 0.35; gun[62].shotspeedup = 0; gun[62].critup = 0; gun[62].dodgeup = 0.25; gun[62].to = 1;
            gun[62].skilltype = 26; gun[62].skillrate = 0.64; gun[62].skilltime = 8; gun[62].skillupallshotspeed = 1 + 0.16; gun[62].skillcontent = "提升己方16%射速";
            gun[63].name = "FNP-9"; gun[63].what = 4; gun[63].hp = 60; gun[63].damage = 28; gun[63].hit = 53; gun[63].dodge = 83; gun[63].shotspeed = 61; gun[63].crit = 0.2; gun[63].belt = 0; gun[63].number = 5; gun[63].effect0 = 2; gun[63].effect1 = 3; gun[63].effect2 = 6; gun[63].effect3 = 8; gun[63].effect4 = 9; gun[63].damageup = 0; gun[63].hitup = 0; gun[63].shotspeedup = 0.1; gun[63].critup = 0; gun[63].dodgeup = 0; gun[63].to = 1;
            gun[63].skilltype = 22;gun[63].skillrate = 0.72;gun[63].skilltime = 7.2;gun[63].skilldownallenemydodge = 0.72;gun[63].skillcontent = "降低敌方72%闪避";
            gun[64].name = "MP-446"; gun[64].what = 4; gun[64].hp = 66; gun[64].damage = 29; gun[64].hit = 51; gun[64].dodge = 72; gun[64].shotspeed = 59; gun[64].crit = 0.2; gun[64].belt = 0; gun[64].number = 5; gun[64].effect0 = 1; gun[64].effect1 = 2; gun[64].effect2 = 4; gun[64].effect3 = 7; gun[64].effect4 = 8; gun[64].damageup = 0.2; gun[64].hitup = 0; gun[64].shotspeedup = 0; gun[64].critup = 0; gun[64].dodgeup = 0; gun[64].to = 1;
            gun[64].skilltype = 17;gun[64].skillrate = 0.38;gun[64].skilltime = 4.8;gun[64].skilldownallenemyshotspeed = 0.4;gun[64].skillcontent = "降低敌方40%射速";
            gun[65].name = "西蒙诺夫"; gun[65].what = 5; gun[65].hp = 93; gun[65].damage = 100; gun[65].hit = 59; gun[65].dodge = 34; gun[65].shotspeed = 34; gun[65].crit = 0.4; gun[65].belt = 0; gun[65].number = 2;gun[65].effect0 = 2;gun[65].effect1 = 8; gun[65].rateup = 0.18; gun[65].damageup = 0; gun[65].hitup = 0; gun[65].shotspeedup = 0; gun[65].critup = 0; gun[65].dodgeup = 0; gun[65].to = 4;
            gun[65].skilltype = 18; gun[65].skillrate = 0.58; gun[65].skilltime = 8; gun[65].skillupmyshotspeed = 1 + 0.64; gun[65].skillcontent = "提升自身射速64%";
            gun[66].name = "FN-49"; gun[66].what = 5; gun[66].hp = 93; gun[66].damage = 111; gun[66].hit = 61; gun[66].dodge = 32; gun[66].shotspeed = 32; gun[66].crit = 0.4; gun[66].belt = 0; gun[66].number = 2; gun[66].effect0 = 3; gun[66].effect1 = 9; gun[66].rateup = 0.18; gun[66].damageup = 0; gun[66].hitup = 0; gun[66].shotspeedup = 0; gun[66].critup = 0; gun[66].dodgeup = 0; gun[66].to = 4;
            gun[66].skilltype = 6; gun[66].skillrate = 0.58; gun[66].skilltime = 3.2; gun[66].skillupmydamage = 1 + 1.6; gun[66].skillcontent = "提升自身伤害160%";
            gun[67].name = "李-恩菲尔德"; gun[67].what = 5; gun[67].hp = 80; gun[67].damage = 135; gun[67].hit = 78; gun[67].dodge = 40; gun[67].shotspeed = 36; gun[67].crit = 0.4; gun[67].belt = 0; gun[67].number = 2; gun[67].effect0 = 2; gun[67].effect1 = 8; gun[65].rateup = 0.25; gun[67].damageup = 0; gun[67].hitup = 0; gun[67].shotspeedup = 0; gun[67].critup = 0; gun[67].dodgeup = 0; gun[67].to = 4;
            gun[67].skilltype = 6; gun[67].skillrate = 0.64; gun[67].skilltime = 3.2; gun[67].skillupmydamage = 1 + 1.92; gun[67].skillcontent = "提升自身伤害192%";
            gun[68].name = "NTW-20"; gun[68].what = 5; gun[68].hp = 93; gun[68].damage = 165; gun[68].hit = 75; gun[68].dodge = 29; gun[68].shotspeed = 30; gun[68].crit = 0.4; gun[68].belt = 0; gun[68].number = 1;gun[68].effect0 = 6;gun[68].rateup = 0.25; gun[68].damageup = 0; gun[68].hitup = 0; gun[68].shotspeedup = 0; gun[68].critup = 0; gun[68].dodgeup = 0; gun[68].to = 4;
            gun[68].skilltype = 1; gun[68].skilldamage = gun[68].damage * 8.8; gun[68].skillcircle = 0; gun[68].skillrate = 0.35; gun[68].skillcontent = "瞄准射击2s对当前目标";
            gun[69].name = "PTRD"; gun[69].what = 5; gun[69].hp = 93; gun[69].damage = 159; gun[69].hit = 75; gun[69].dodge = 29; gun[69].shotspeed = 28; gun[69].crit = 0.4; gun[69].belt = 0; gun[69].number = 1; gun[69].effect0 = 6; gun[69].rateup = 0.22; gun[69].damageup = 0; gun[69].hitup = 0; gun[69].shotspeedup = 0; gun[69].critup = 0; gun[69].dodgeup = 0; gun[69].to = 4;
            gun[69].skilltype = 1; gun[69].skilldamage = gun[69].damage * 7.7; gun[69].skillcircle = 0; gun[69].skillrate = 0.35; gun[69].skillcontent = "瞄准射击2s对当前目标";
            gun[70].name = "SVT-38"; gun[70].what = 5; gun[70].hp = 84; gun[70].damage = 110; gun[70].hit = 59; gun[70].dodge = 34; gun[70].shotspeed = 34; gun[70].crit = 0.4; gun[70].belt = 0; gun[70].number = 1; gun[70].effect0 = 6; gun[70].rateup = 0.18; gun[70].damageup = 0; gun[70].hitup = 0; gun[70].shotspeedup = 0; gun[70].critup = 0; gun[70].dodgeup = 0; gun[70].to = 4;
            gun[70].skilltype = 1; gun[70].skilldamage = gun[70].damage * 4.4; gun[70].skillcircle = 0; gun[70].skillrate = 0.66; gun[70].skillcontent = "阻断射击2s对特定目标";
            gun[71].name = "WA2000"; gun[71].what = 5; gun[71].hp = 88; gun[71].damage = 130; gun[71].hit = 82; gun[71].dodge = 30; gun[71].shotspeed = 39; gun[71].crit = 0.4; gun[71].belt = 0; gun[71].number = 1; gun[71].effect0 = 6; gun[71].rateup = 0.25; gun[71].damageup = 0; gun[71].hitup = 0; gun[71].shotspeedup = 0; gun[71].critup = 0; gun[71].dodgeup = 0; gun[71].to = 4;
            gun[71].skilltype = 20; gun[71].skillrate = 0.64; gun[71].skilltime = 12.8; gun[71].skilldownonedodge = 0.96; gun[71].skillcontent = "降低当前目标96%闪避,不算";
            gun[72].name = "M14"; gun[72].what = 5; gun[72].hp = 93; gun[72].damage = 108; gun[72].hit = 71; gun[72].dodge = 27; gun[72].shotspeed = 43; gun[72].crit = 0.4; gun[72].belt = 0; gun[72].number = 2; gun[72].effect0 = 3; gun[72].effect1 = 9; gun[72].rateup = 0.2; gun[72].damageup = 0; gun[72].hitup = 0; gun[72].shotspeedup = 0; gun[72].critup = 0; gun[72].dodgeup = 0; gun[72].to = 4;
            gun[72].skilltype = 6; gun[72].skillrate = 0.64; gun[72].skilltime = 3.2; gun[72].skillupmydamage = 1 + 1.6; gun[72].skillcontent = "提升自身伤害160%";
            gun[73].name = "M21"; gun[73].what = 5; gun[73].hp = 93; gun[73].damage = 118; gun[73].hit = 74; gun[73].dodge = 27; gun[73].shotspeed = 35; gun[73].crit = 0.4; gun[73].belt = 0; gun[73].number = 2; gun[73].effect0 = 2; gun[73].effect1 = 8; gun[73].rateup = 0.2; gun[73].damageup = 0; gun[73].hitup = 0; gun[73].shotspeedup = 0; gun[73].critup = 0; gun[73].dodgeup = 0; gun[73].to = 4;
            gun[73].skilltype = 1; gun[73].skilldamage = gun[73].damage * 4.8; gun[73].skillcircle = 0; gun[73].skillrate = 0.66; gun[73].skillcontent = "阻断射击2s对特定目标";
            gun[74].name = "BM59"; gun[74].what = 5; gun[74].hp = 93; gun[74].damage = 118; gun[74].hit = 73; gun[74].dodge = 27; gun[74].shotspeed = 35; gun[74].crit = 0.4; gun[74].belt = 0; gun[74].number = 1;gun[74].effect0 = 6;gun[74].rateup = 0.18; gun[74].damageup = 0; gun[74].hitup = 0; gun[74].shotspeedup = 0; gun[74].critup = 0; gun[74].dodgeup = 0; gun[74].to = 4;
            gun[74].skilltype = 18; gun[74].skillrate = 0.58; gun[74].skilltime = 8; gun[74].skillupmyshotspeed = 1 + 0.64; gun[74].skillcontent = "提升自身射速64%";
            gun[75].name = "M1加兰德"; gun[75].what = 5; gun[75].hp = 88; gun[75].damage = 120; gun[75].hit = 62; gun[75].dodge = 28; gun[75].shotspeed = 37; gun[75].crit = 0.4; gun[75].belt = 0; gun[75].number = 1; gun[75].effect0 = 6; gun[75].rateup = 0.2; gun[75].damageup = 0; gun[75].hitup = 0; gun[75].shotspeedup = 0; gun[75].critup = 0; gun[75].dodgeup = 0; gun[75].to = 4;
            gun[75].skilltype = 1; gun[75].skilldamage = gun[75].damage * 4.8; gun[75].skillcircle = 0; gun[75].skillrate = 0.66; gun[75].skillcontent = "瞄准射击2s对当前目标";
            gun[76].name = "SV-98"; gun[76].what = 5; gun[76].hp = 84; gun[76].damage = 122; gun[76].hit = 74; gun[76].dodge = 28; gun[76].shotspeed = 37; gun[76].crit = 0.4; gun[76].belt = 0; gun[76].number = 1; gun[76].effect0 = 9; gun[76].rateup = 0.2; gun[76].damageup = 0; gun[76].hitup = 0; gun[76].shotspeedup = 0; gun[76].critup = 0; gun[76].dodgeup = 0; gun[76].to = 4;
            gun[76].skilltype = 1; gun[76].skilldamage = gun[76].damage * 4.8; gun[76].skillcircle = 0; gun[76].skillrate = 0.66; gun[76].skillcontent = "瞄准射击2s对当前目标";
            gun[77].name = "G43"; gun[77].what = 5; gun[77].hp = 80; gun[77].damage = 111; gun[77].hit = 58; gun[77].dodge = 28; gun[77].shotspeed = 40; gun[77].crit = 0.4; gun[77].belt = 0; gun[77].number = 2; gun[77].effect0 = 3; gun[77].effect1 = 9; gun[77].rateup = 0.2; gun[77].damageup = 0; gun[77].hitup = 0; gun[77].shotspeedup = 0; gun[77].critup = 0; gun[77].dodgeup = 0; gun[77].to = 4;
            gun[77].skilltype = 18; gun[77].skillrate = 0.58; gun[77].skilltime = 12.8; gun[77].skillupmyshotspeed = 1 + 0.8; gun[77].skillcontent = "(夜)提升自身射速80%";
            gun[78].name = "汉阳造"; gun[78].what = 5; gun[78].hp = 102; gun[78].damage = 108; gun[78].hit = 60; gun[78].dodge = 37; gun[78].shotspeed = 31; gun[78].crit = 0.4; gun[78].belt = 0; gun[78].number = 1; gun[78].effect0 = 6; gun[78].rateup = 0.22; gun[78].damageup = 0; gun[78].hitup = 0; gun[78].shotspeedup = 0; gun[78].critup = 0; gun[78].dodgeup = 0; gun[78].to = 4;
            gun[78].skilltype = 7;gun[78].skillrate = 0.75;gun[78].skilltime = 7.5;gun[78].skilldownonehit = 0.9; gun[78].skillcontent = "(夜)降低目标命中90%,不算"; 
            gun[79].name = "Kar98k"; gun[79].what = 5; gun[79].hp = 84; gun[79].damage = 135; gun[79].hit = 78; gun[79].dodge = 41; gun[79].shotspeed = 34; gun[79].crit = 0.4; gun[79].belt = 0; gun[79].number = 2; gun[79].effect0 = 3; gun[79].effect1 = 9; gun[79].rateup = 0.25; gun[79].damageup = 0; gun[79].hitup = 0; gun[79].shotspeedup = 0; gun[79].critup = 0; gun[79].dodgeup = 0; gun[79].to = 4;
            gun[79].skilltype = 1; gun[79].skilldamage = gun[79].damage * 8.8; gun[79].skillcircle = 0; gun[79].skillrate = 0.35; gun[79].skillcontent = "瞄准射击2s对当前目标";
            gun[80].name = "莫辛纳甘"; gun[80].what = 5; gun[80].hp = 88; gun[80].damage = 131; gun[80].hit = 85; gun[80].dodge = 38; gun[80].shotspeed = 30; gun[80].crit = 0.4; gun[80].belt = 0; gun[80].number = 1; gun[80].effect0 = 9; gun[80].rateup = 0.22; gun[80].damageup = 0; gun[80].hitup = 0; gun[80].shotspeedup = 0; gun[80].critup = 0; gun[80].dodgeup = 0; gun[80].to = 4;
            gun[80].skilltype = 1; gun[80].skilldamage = gun[80].damage * 5.3; gun[80].skillcircle = 0; gun[80].skillrate = 0.66; gun[80].skillcontent = "定点射击2s对最远目标";
            gun[81].name = "春田"; gun[81].what = 5; gun[81].hp = 84; gun[81].damage = 128; gun[81].hit = 72; gun[81].dodge = 40; gun[81].shotspeed = 32; gun[81].crit = 0.4; gun[81].belt = 0; gun[81].number = 1; gun[81].effect0 = 3; gun[81].rateup = 0.22;  gun[81].damageup = 0; gun[81].hitup = 0; gun[81].shotspeedup = 0; gun[81].critup = 0; gun[81].dodgeup = 0; gun[81].to = 4;
            gun[81].skilltype = 1; gun[81].skilldamage = gun[81].damage * 5.3; gun[81].skillcircle = 0; gun[81].skillrate = 0.66; gun[81].skillcontent = "定点射击2s对最远目标";
            gun[82].name = "M60"; gun[82].what = 6; gun[82].hp = 182; gun[82].damage = 92; gun[82].hit = 26; gun[82].dodge = 26; gun[82].shotspeed = 111; gun[82].crit = 0.05; gun[82].belt = 9; gun[82].number = 0; gun[82].damageup = 0; gun[82].hitup = 0; gun[82].shotspeedup = 0; gun[82].critup = 0; gun[82].dodgeup = 0; gun[82].to = 0;
            gun[82].skilltype = 6; gun[82].skillrate = 0.66; gun[82].skilltime = 8; gun[82].skillupmydamage = 1 + 1.1; gun[82].skillcontent = "(夜)提升自身伤害110%";
            gun[83].name = "MG5"; gun[83].what = 6; gun[83].hp = 198; gun[83].damage = 85; gun[83].hit = 27; gun[83].dodge = 25; gun[83].shotspeed = 120; gun[83].crit = 0.05; gun[83].belt = 11; gun[83].number = 0; gun[83].damageup = 0; gun[83].hitup = 0; gun[83].shotspeedup = 0; gun[83].critup = 0; gun[83].dodgeup = 0; gun[83].to = 0;
            gun[83].skilltype = 6; gun[83].skillrate = 0.7; gun[83].skilltime = 8; gun[83].skillupmydamage = 1 + 0.66; gun[83].skillcontent = "提升自身伤害66%";
            gun[84].name = "M1918"; gun[84].what = 6; gun[84].hp = 157; gun[84].damage = 96; gun[84].hit = 31; gun[84].dodge = 33; gun[84].shotspeed = 114; gun[84].crit = 0.05; gun[84].belt = 8; gun[84].number = 0; gun[84].damageup = 0; gun[84].hitup = 0; gun[84].shotspeedup = 0; gun[84].critup = 0; gun[84].dodgeup = 0; gun[84].to = 0;
            gun[84].skilltype = 6; gun[84].skillrate = 0.66; gun[84].skilltime = 8; gun[84].skillupmydamage = 1 + 0.66; gun[84].skillcontent = "提升自身伤害66%";
            gun[85].name = "MG3"; gun[85].what = 6; gun[85].hp = 198; gun[85].damage = 85; gun[85].hit = 26; gun[85].dodge = 21; gun[85].shotspeed = 130; gun[85].crit = 0.05; gun[85].belt = 10; gun[85].number = 0; gun[85].damageup = 0; gun[85].hitup = 0; gun[85].shotspeedup = 0; gun[85].critup = 0; gun[85].dodgeup = 0; gun[85].to = 0;
            gun[85].skilltype = 6; gun[85].skillrate = 0.66; gun[85].skilltime = 8; gun[85].skillupmydamage = 1 + 0.66; gun[85].skillcontent = "提升自身伤害66%";
            gun[86].name = "M1919A4"; gun[86].what = 6; gun[86].hp = 174; gun[86].damage = 96; gun[86].hit = 26; gun[86].dodge = 22; gun[86].shotspeed = 99; gun[86].crit = 0.05; gun[86].belt = 9; gun[86].number = 0; gun[86].damageup = 0; gun[86].hitup = 0; gun[86].shotspeedup = 0; gun[86].critup = 0; gun[86].dodgeup = 0; gun[86].to = 0;
            gun[86].skilltype = 9; gun[86].skilltime = 8; gun[86].skillrate = 0.96; gun[86].skillupmyhit = 1 + 3; gun[86].skillcontent = "提升自身命中300%";
            gun[87].name = "PK"; gun[87].what = 6; gun[87].hp = 190; gun[87].damage = 93; gun[87].hit = 21; gun[87].dodge = 22; gun[87].shotspeed = 83; gun[87].crit = 0.05; gun[87].belt = 11; gun[87].number = 0; gun[87].damageup = 0; gun[87].hitup = 0; gun[87].shotspeedup = 0; gun[87].critup = 0; gun[87].dodgeup = 0; gun[87].to = 0;
            gun[87].skilltype = 6; gun[87].skillrate = 0.66; gun[87].skilltime = 8; gun[87].skillupmydamage = 1 + 0.66; gun[87].skillcontent = "提升自身伤害66%";
            gun[88].name = "内格夫"; gun[88].what = 6; gun[88].hp = 174; gun[88].damage = 84; gun[88].hit = 35; gun[88].dodge = 36; gun[88].shotspeed = 139; gun[88].crit = 0.05; gun[88].belt = 9; gun[88].number = 0; gun[88].damageup = 0; gun[88].hitup = 0; gun[88].shotspeedup = 0; gun[88].critup = 0; gun[88].dodgeup = 0; gun[88].to = 0;
            gun[88].skilltype = 9; gun[88].skilltime = 8; gun[88].skillrate = 0.96; gun[88].skillupmyhit = 1 + 3.6; gun[88].skillcontent = "提升自身命中360%";
            gun[89].name = "RPD"; gun[89].what = 6; gun[89].hp = 165; gun[89].damage = 82; gun[89].hit = 34; gun[89].dodge = 34; gun[89].shotspeed = 121; gun[89].crit = 0.05; gun[89].belt = 8; gun[89].number = 0; gun[89].damageup = 0; gun[89].hitup = 0; gun[89].shotspeedup = 0; gun[89].critup = 0; gun[89].dodgeup = 0; gun[89].to = 0;
            gun[89].skilltype = 6; gun[89].skillrate = 0.66; gun[89].skilltime = 8; gun[89].skillupmydamage = 1 + 0.616; gun[89].skillcontent = "提升自身伤害61.6%";
            gun[90].name = "M2HB"; gun[90].what = 6; gun[90].hp = 215; gun[90].damage = 102; gun[90].hit = 18; gun[90].dodge = 16; gun[90].shotspeed = 100; gun[90].crit = 0.05; gun[90].belt = 9; gun[90].number = 0; gun[90].damageup = 0; gun[90].hitup = 0; gun[90].shotspeedup = 0; gun[90].critup = 0; gun[90].dodgeup = 0; gun[90].to = 0;
            gun[90].skilltype = 9; gun[90].skilltime = 8; gun[90].skillrate = 0.8; gun[90].skillupmyhit = 1 + 2.5; gun[90].skillcontent = "提升自身命中250%";
            gun[91].name = "LWMMG"; gun[91].what = 6; gun[91].hp = 174; gun[91].damage = 92; gun[91].hit = 23; gun[91].dodge = 22; gun[91].shotspeed = 89; gun[91].crit = 0.05; gun[91].belt = 9; gun[91].number = 0; gun[91].damageup = 0; gun[91].hitup = 0; gun[91].shotspeedup = 0; gun[91].critup = 0; gun[91].dodgeup = 0; gun[91].to = 0;
            gun[91].skilltype = 9; gun[91].skilltime = 8; gun[91].skillrate = 0.9; gun[91].skillupmyhit = 1 + 3; gun[91].skillcontent = "提升自身命中300%";
            gun[92].name = "M249 SAW"; gun[92].what = 6; gun[92].hp = 157; gun[92].damage = 79; gun[92].hit = 35; gun[92].dodge = 36; gun[92].shotspeed = 139; gun[92].crit = 0.05; gun[92].belt = 8; gun[92].number = 0; gun[92].damageup = 0; gun[92].hitup = 0; gun[92].shotspeedup = 0; gun[92].critup = 0; gun[92].dodgeup = 0; gun[92].to = 0;
            gun[92].skilltype = 9; gun[92].skilltime = 10; gun[92].skillrate = 0.9; gun[92].skillupmyhit = 1 + 4.5 ; gun[92].skillcontent = "(夜)提升自身命中450%";
            gun[93].name = "AAT-52"; gun[93].what = 6; gun[93].hp = 182; gun[93].damage = 79; gun[93].hit = 22; gun[93].dodge = 22; gun[93].shotspeed = 118; gun[93].crit = 0.05; gun[93].belt = 10; gun[93].number = 0; gun[93].damageup = 0; gun[93].hitup = 0; gun[93].shotspeedup = 0; gun[93].critup = 0; gun[93].dodgeup = 0; gun[93].to = 0;
            gun[93].skilltype = 6; gun[93].skillrate = 0.66; gun[93].skilltime = 8; gun[93].skillupmydamage = 1 + 0.55; gun[93].skillcontent = "提升自身伤害55%";
            gun[94].name = "DP28"; gun[94].what = 6; gun[94].hp = 141; gun[94].damage = 88; gun[94].hit = 28; gun[94].dodge = 31; gun[94].shotspeed = 114; gun[94].crit = 0.05; gun[94].belt = 9; gun[94].number = 0; gun[94].damageup = 0; gun[94].hitup = 0; gun[94].shotspeedup = 0; gun[94].critup = 0; gun[94].dodgeup = 0; gun[94].to = 0;
            gun[94].skilltype = 7; gun[94].skillrate = 0.45; gun[94].skilltime = 7.5; gun[94].skilldownonehit = 0.9; gun[94].skillcontent = "降低目标命中90%,不算";
            gun[95].name = "MG42"; gun[95].what = 6; gun[95].hp = 165; gun[95].damage = 87; gun[95].hit = 23; gun[95].dodge = 26; gun[95].shotspeed = 132; gun[95].crit = 0.05; gun[95].belt = 10; gun[95].number = 0; gun[95].damageup = 0; gun[95].hitup = 0; gun[95].shotspeedup = 0; gun[95].critup = 0; gun[95].dodgeup = 0; gun[95].to = 0;
            gun[95].skilltype = 23; gun[95].skillrate = 0.58; gun[95].skilltime = 7.2; gun[95].skillupmydodge = 1 + 3.6; gun[95].skillcontent = "提升自身闪避360%";
            gun[96].name = "MG34"; gun[96].what = 6; gun[96].hp = 157; gun[96].damage = 85; gun[96].hit = 22; gun[96].dodge = 25; gun[96].shotspeed = 120; gun[96].crit = 0.05; gun[96].belt = 10; gun[96].number = 0; gun[96].damageup = 0; gun[96].hitup = 0; gun[96].shotspeedup = 0; gun[96].critup = 0; gun[96].dodgeup = 0; gun[96].to = 0;
            gun[96].skilltype = 3; gun[96].skillrate = 0.64; gun[96].skilltime = 4; gun[96].skilldownonedamage = 0.96; gun[96].skillcontent = "降低当前目标96%伤害,不算";
            gun[97].name = "布伦"; gun[97].what = 6; gun[97].hp = 174; gun[97].damage = 81; gun[97].hit = 29; gun[97].dodge = 28; gun[97].shotspeed = 102; gun[97].crit = 0.05; gun[97].belt = 8; gun[97].number = 0; gun[97].damageup = 0; gun[97].hitup = 0; gun[97].shotspeedup = 0; gun[97].critup = 0; gun[97].dodgeup = 0; gun[97].to = 0;
            gun[97].skilltype = 16; gun[97].skillrate = 0.67; gun[97].skilltime = 4; gun[97].skilldownoneshotspeed = 0.96; gun[97].skillcontent = "降低当前目标96%射速,不算";
            gun[98].name = "FG42"; gun[98].what = 6; gun[98].hp = 149; gun[98].damage = 80; gun[98].hit = 25; gun[98].dodge = 30; gun[98].shotspeed = 116; gun[98].crit = 0.05; gun[98].belt = 8; gun[98].number = 0; gun[98].damageup = 0; gun[98].hitup = 0; gun[98].shotspeedup = 0; gun[98].critup = 0; gun[98].dodgeup = 0; gun[98].to = 0;
            gun[98].skilltype = 23; gun[98].skillrate = 0.58; gun[98].skilltime = 7.2; gun[98].skillupmydodge = 1 + 3.6; gun[98].skillcontent = "提升自身闪避360%";
            gun[99].name = "MK48"; gun[99].what = 6; gun[99].hp = 174; gun[99].damage = 90; gun[99].hit = 24; gun[99].dodge = 26; gun[99].shotspeed = 111; gun[99].crit = 0.05; gun[99].belt = 10; gun[99].number = 0; gun[99].damageup = 0; gun[99].hitup = 0; gun[99].shotspeedup = 0; gun[99].critup = 0; gun[99].dodgeup = 0; gun[99].to = 0;
            gun[99].skilltype = 9; gun[99].skilltime = 8; gun[99].skillrate = 0.9; gun[99].skillupmyhit = 1 + 3.6; gun[99].skillcontent = "提升自身命中360%";
            gun[100].name = "谢尔久科夫"; gun[100].what = 4; gun[100].hp = 70; gun[100].damage = 33; gun[100].hit = 58; gun[100].dodge = 68; gun[100].shotspeed = 59; gun[100].crit = 0.2; gun[100].belt = 0; gun[100].number = 3; gun[100].effect0 = 2; gun[100].effect1 = 4; gun[100].effect2 = 8; gun[100].damageup = 0.2; gun[100].hitup = 0.3; gun[100].to = 1;
            gun[100].skilltype = 4; gun[100].skillrate = 0.56; gun[100].skilltime = 8; gun[100].skillupalldamage = 1 + 0.2; gun[100].skillcontent = "提升己方20%伤害";
            for (int i = 0; i < 102; i++)
            {
                Combo0.Items.Add(gun[i].name);
                Combo1.Items.Add(gun[i].name);
                Combo2.Items.Add(gun[i].name);
                Combo3.Items.Add(gun[i].name);
                Combo4.Items.Add(gun[i].name);
                Combo5.Items.Add(gun[i].name);
                Combo6.Items.Add(gun[i].name);
                Combo7.Items.Add(gun[i].name);
                Combo8.Items.Add(gun[i].name);
            }


            for (int i = 0; i < 9; i++)
            {
                gg[i] = new GunGrid();
                gg[i].critup = 1.00;
                gg[i].damageup = 1.00;
                gg[i].dodgeup = 1.00;
                gg[i].hitup = 1.00;
                gg[i].shotspeedup = 1.00;
                gg[i].rateup = 0.00;
                lastgunindex[i] = -1;
                skillupdamage[i] = new Double();
                skillupdodge[i] = new Double();
                skilluphit[i] = new Double();
                skillupshotspeed[i] = new double();
                skillupshotspeed[i] = 0;
                skilluphit[i] = 0;
                skillupdodge[i] = 0;
                skillupdamage[i] = 0;

            }

            skilldowndodge = 1;
            skilldownhit = 1;
        }



        public void othercombochange(int nextselect,int select,int grid,int ggi){

            switch (gun[nextselect].number)
            {
                      
                case 1:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                            }
                        break;
                    }
                case 2:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                case 3:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect2 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                case 4:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect2 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect3 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                case 5:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect2 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect3 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect4 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                case 6:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect2 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect3 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect4 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect5 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                case 7:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect2 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect3 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect4 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect5 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect6 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                case 8:
                    {
                        if (gun[nextselect].effect0 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect1 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect2 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect3 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect4 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect5 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect6 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        else if (gun[nextselect].effect7 == grid && (gun[nextselect].to == gun[select].what || gun[nextselect].to == 1))
                        {
                            gg[ggi].critup += gun[nextselect].critup;
                            gg[ggi].damageup += gun[nextselect].damageup;
                            gg[ggi].dodgeup += gun[nextselect].dodgeup;
                            gg[ggi].shotspeedup += gun[nextselect].shotspeedup;
                            gg[ggi].hitup += gun[nextselect].hitup;
                            gg[ggi].rateup += gun[nextselect].rateup;
                        }
                        break;
                    }
                default:
                    break;
            }


        }

        public void renewindex(int comboi)
        {
            int select = 0;
            switch(comboi)
            {
                case 0:
                    {
                        select = Combo0.SelectedIndex;
                        Ldamage0.Content = (gun[select].damage * (gg[0].damageup + skillupdamage[0])).ToString("0.00");
                        Lhit0.Content = (gun[select].hit * (gg[0].hitup + skilluphit[0])).ToString("0.00");
                        Lskillread0.Content = gun[select].skillcontent;
                        Lskilldamage0.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime0.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed * (gg[0].shotspeedup + skillupshotspeed[0]) > 120)
                            Lshotspeed0.Content = 120.00;
                        else
                            Lshotspeed0.Content = (gun[select].shotspeed * (gg[0].shotspeedup + skillupshotspeed[0])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[0].rateup > 1)
                            Lskillrate0.Content = "100%";
                        else
                            Lskillrate0.Content = ((gun[select].skillrate + gg[0].rateup) * 100).ToString() + "%";
                        Lcrit0.Content = (gun[select].crit * gg[0].critup).ToString("0.00");
                        Ldodge0.Content = (gun[select].dodge * (gg[0].dodgeup + skillupdodge[0])).ToString("0.00");
                        Lhp0.Content = gun[select].hp;
                        Lbelt0.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text)*skilldowndodge).ToString("0.00");
                        Lindex0.Content = (Index(Double.Parse(Lshotspeed0.Content.ToString()), Double.Parse(Ldamage0.Content.ToString()), Double.Parse(Lcrit0.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit0.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 1:
                    {
                        select = Combo1.SelectedIndex;
                        Ldamage1.Content = (gun[select].damage * (gg[1].damageup+skillupdamage[1])).ToString("0.00");
                        Lhit1.Content = (gun[select].hit * (gg[1].hitup+skilluphit[1])).ToString("0.00");
                        Lskillread1.Content = gun[select].skillcontent;
                        Lskilldamage1.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime1.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed * (gg[1].shotspeedup + skillupshotspeed[1]) > 120)
                            Lshotspeed1.Content = 120.00;
                        else
                           Lshotspeed1.Content = (gun[select].shotspeed * (gg[1].shotspeedup + skillupshotspeed[1])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[1].rateup > 1)
                            Lskillrate1.Content = "100%";
                        else
                            Lskillrate1.Content = ((gun[select].skillrate + gg[1].rateup) * 100).ToString() + "%";
                        Lcrit1.Content = (gun[select].crit * gg[1].critup).ToString("0.00");
                        Ldodge1.Content = (gun[select].dodge * (gg[1].dodgeup+skillupdodge[1])).ToString("0.00");
                        Lhp1.Content = gun[select].hp;
                        Lbelt1.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex1.Content = (Index(Double.Parse(Lshotspeed1.Content.ToString()), Double.Parse(Ldamage1.Content.ToString()), Double.Parse(Lcrit1.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit1.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 2:
                    {
                        select = Combo2.SelectedIndex;
                        Ldamage2.Content = (gun[select].damage * (gg[2].damageup+skillupdamage[2])).ToString("0.00");
                        Lhit2.Content = (gun[select].hit * (gg[2].hitup+skilluphit[2])).ToString("0.00");
                        Lskillread2.Content = gun[select].skillcontent;
                        Lskilldamage2.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime2.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed *( gg[2].shotspeedup + skillupshotspeed[2]) > 120)
                               Lshotspeed2.Content = 120.00;
                        else
                               Lshotspeed2.Content = (gun[select].shotspeed *( gg[2].shotspeedup + skillupshotspeed[2])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[2].rateup > 1)
                            Lskillrate2.Content = "100%";
                        else
                            Lskillrate2.Content = ((gun[select].skillrate + gg[2].rateup) * 100).ToString() + "%";
                        Lcrit2.Content = (gun[select].crit * gg[2].critup).ToString("0.00");
                        Ldodge2.Content = (gun[select].dodge *(gg[2].dodgeup+skillupdodge[2])).ToString("0.00");
                        Lhp2.Content = gun[select].hp;
                        Lbelt2.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex2.Content = (Index(Double.Parse(Lshotspeed2.Content.ToString()), Double.Parse(Ldamage2.Content.ToString()), Double.Parse(Lcrit2.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit2.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 3:
                    {
                        select = Combo3.SelectedIndex;
                        Ldamage3.Content = (gun[select].damage * (gg[3].damageup+skillupdamage[3])).ToString("0.00");
                        Lhit3.Content = (gun[select].hit * (gg[3].hitup+skilluphit[3])).ToString("0.00");
                        Lskillread3.Content = gun[select].skillcontent;
                        Lskilldamage3.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime3.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed *( gg[3].shotspeedup + skillupshotspeed[3]) > 120)
                            Lshotspeed3.Content = 120.00;
                        else
                              Lshotspeed3.Content = (gun[select].shotspeed * (gg[3].shotspeedup + skillupshotspeed[3])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[3].rateup > 1)
                            Lskillrate3.Content = "100%";
                        else
                            Lskillrate3.Content = ((gun[select].skillrate + gg[3].rateup) * 100).ToString() + "%";
                        Lcrit3.Content = (gun[select].crit * gg[3].critup).ToString("0.00");
                        Ldodge3.Content = (gun[select].dodge *( gg[3].dodgeup+skillupdodge[3])).ToString("0.00");
                        Lhp3.Content = gun[select].hp;
                        Lbelt3.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex3.Content = (Index(Double.Parse(Lshotspeed3.Content.ToString()), Double.Parse(Ldamage3.Content.ToString()), Double.Parse(Lcrit3.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit3.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 4:
                    {
                        select = Combo4.SelectedIndex;
                        Ldamage4.Content = (gun[select].damage * (gg[4].damageup+skillupdamage[4])).ToString("0.00");
                        Lhit4.Content = (gun[select].hit *(gg[4].hitup+skilluphit[4])).ToString("0.00");
                        Lskillread4.Content = gun[select].skillcontent;
                        Lskilldamage4.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime4.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed * (gg[4].shotspeedup + skillupshotspeed[4]) > 120)
                            Lshotspeed4.Content = 120.00;
                        else
                           Lshotspeed4.Content = (gun[select].shotspeed * (gg[4].shotspeedup + skillupshotspeed[4])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[4].rateup > 1)
                            Lskillrate4.Content = "100%";
                        else
                            Lskillrate4.Content = ((gun[select].skillrate + gg[4].rateup) * 100).ToString() + "%";
                        Lcrit4.Content = (gun[select].crit * gg[4].critup).ToString("0.00");
                        Ldodge4.Content = (gun[select].dodge *( gg[4].dodgeup+skillupdodge[4])).ToString("0.00");
                        Lhp4.Content = gun[select].hp;
                        Lbelt4.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex4.Content = (Index(Double.Parse(Lshotspeed4.Content.ToString()), Double.Parse(Ldamage4.Content.ToString()), Double.Parse(Lcrit4.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit4.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 5:
                    {
                        select = Combo5.SelectedIndex;
                        Ldamage5.Content = (gun[select].damage * (gg[5].damageup+skillupdamage[5])).ToString("0.00");
                        Lhit5.Content = (gun[select].hit * (gg[5].hitup+skilluphit[5])).ToString("0.00");
                        Lskillread5.Content = gun[select].skillcontent;
                        Lskilldamage5.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime5.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed *( gg[5].shotspeedup + skillupshotspeed[5]) > 120)
                            Lshotspeed5.Content = 120.00;
                        else
                            Lshotspeed5.Content = (gun[select].shotspeed * (gg[5].shotspeedup + skillupshotspeed[5])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[5].rateup > 1)
                            Lskillrate5.Content = "100%";
                        else
                            Lskillrate5.Content = ((gun[select].skillrate + gg[5].rateup) * 100).ToString() + "%";
                        Lcrit5.Content = (gun[select].crit * gg[5].critup).ToString("0.00");
                        Ldodge5.Content = (gun[select].dodge *( gg[5].dodgeup+skillupdodge[5])).ToString("0.00");
                        Lhp5.Content = gun[select].hp;
                        Lbelt5.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex5.Content = (Index(Double.Parse(Lshotspeed5.Content.ToString()), Double.Parse(Ldamage5.Content.ToString()), Double.Parse(Lcrit5.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit5.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 6:
                    {
                        select = Combo6.SelectedIndex;
                        Ldamage6.Content = (gun[select].damage * (gg[6].damageup+skillupdamage[6])).ToString("0.00");
                        Lhit6.Content = (gun[select].hit *( gg[6].hitup+skilluphit[6])).ToString("0.00");
                        Lskillread6.Content = gun[select].skillcontent;
                        Lskilldamage6.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime6.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed * (gg[6].shotspeedup + skillupshotspeed[6]) > 120)
                            Lshotspeed6.Content = 120.00;
                        else
                          Lshotspeed6.Content = (gun[select].shotspeed * (gg[6].shotspeedup + skillupshotspeed[6])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[6].rateup > 1)
                            Lskillrate6.Content = "100%";
                        else
                            Lskillrate6.Content = ((gun[select].skillrate + gg[6].rateup) * 100).ToString() + "%";
                        Lcrit6.Content = (gun[select].crit * gg[6].critup).ToString("0.00");
                        Ldodge6.Content = (gun[select].dodge *( gg[6].dodgeup+skillupdodge[6])).ToString("0.00");
                        Lhp6.Content = gun[select].hp;
                        Lbelt6.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex6.Content = (Index(Double.Parse(Lshotspeed6.Content.ToString()), Double.Parse(Ldamage6.Content.ToString()), Double.Parse(Lcrit6.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit6.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 7:
                    {
                        select = Combo7.SelectedIndex;
                        Ldamage7.Content = (gun[select].damage * (gg[7].damageup+skillupdamage[7])).ToString("0.00");
                        Lhit7.Content = (gun[select].hit *( gg[7].hitup+skilluphit[7])).ToString("0.00");
                        Lskillread7.Content = gun[select].skillcontent;
                        Lskilldamage7.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime7.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed * (gg[7].shotspeedup + skillupshotspeed[7]) > 120)
                            Lshotspeed7.Content = 120.00;
                        else
                        Lshotspeed7.Content = (gun[select].shotspeed * (gg[7].shotspeedup + skillupshotspeed[7])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[7].rateup > 1)
                            Lskillrate7.Content = "100%";
                        else
                            Lskillrate7.Content = ((gun[select].skillrate + gg[7].rateup) * 100).ToString() + "%";
                        Lcrit7.Content = (gun[select].crit * gg[7].critup).ToString("0.00");
                        Ldodge7.Content = (gun[select].dodge *( gg[7].dodgeup+skillupdodge[7])).ToString("0.00");
                        Lhp7.Content = gun[select].hp;
                        Lbelt7.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex7.Content = (Index(Double.Parse(Lshotspeed7.Content.ToString()), Double.Parse(Ldamage7.Content.ToString()), Double.Parse(Lcrit7.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit7.Content.ToString()), gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
                case 8:
                    {
                        select = Combo8.SelectedIndex;
                        Ldamage8.Content = (gun[select].damage * (gg[8].damageup+skillupdamage[8])).ToString("0.00");
                        Lhit8.Content = (gun[select].hit * (gg[8].hitup+skilluphit[8])).ToString("0.00");
                        Lskillread8.Content = gun[select].skillcontent;
                        Lskilldamage8.Content = gun[select].skilldamage.ToString("0.0");
                        Ltime8.Content = gun[select].skilltime;
                        if (gun[select].belt == 0 && gun[select].shotspeed * (gg[8].shotspeedup + skillupshotspeed[8]) > 120)
                            Lshotspeed8.Content = 120.00;
                        else
                        Lshotspeed8.Content = (gun[select].shotspeed *( gg[8].shotspeedup + skillupshotspeed[8])).ToString("0.00");
                        if (gun[select].what == 4 && gun[select].skillrate + gg[8].rateup > 1)
                            Lskillrate8.Content = "100%";
                        else
                            Lskillrate8.Content = ((gun[select].skillrate + gg[8].rateup) * 100).ToString() + "%";
                        Lcrit8.Content = (gun[select].crit * gg[8].critup).ToString("0.00");
                        Ldodge8.Content = (gun[select].dodge * (gg[8].dodgeup+skillupdodge[8])).ToString("0.00");
                        Lhp8.Content = gun[select].hp;
                        Lbelt8.Content = gun[select].belt;
                        nowdodge.Content = (Double.Parse(enemydodge.Text) * skilldowndodge).ToString("0.00");
                        Lindex8.Content = (Index(Double.Parse(Lshotspeed8.Content.ToString()), Double.Parse(Ldamage8.Content.ToString()), Double.Parse(Lcrit8.Content.ToString()), Double.Parse(nowdodge.Content.ToString()), Double.Parse(Lhit8.Content.ToString()),gun[select].belt)).ToString("0.00");
                        allindex.Content = (Double.Parse(Lindex0.Content.ToString()) + Double.Parse(Lindex1.Content.ToString()) + Double.Parse(Lindex2.Content.ToString()) + Double.Parse(Lindex3.Content.ToString()) + Double.Parse(Lindex4.Content.ToString()) + Double.Parse(Lindex5.Content.ToString()) + Double.Parse(Lindex6.Content.ToString()) + Double.Parse(Lindex7.Content.ToString()) + Double.Parse(Lindex8.Content.ToString())).ToString("0.00");
                        break;
                    }
            }
        }
       
        public double Index(double shotspeed,double damage,double crit,double enemydodge,double hit,int belt)
       {
            if (hit == 0)
                return 0;
           else if(belt == 0)
                return shotspeed * damage / 50 * (1 - crit + crit * 1.5) / (1 + enemydodge / hit);
            else
                return belt * damage * (1 - crit + crit * 1.5) / (1 + enemydodge / hit) / ((double)belt / 3.0 + 4 + 200 / shotspeed);
        }

        string getcombogunname(int combo)
        {
            switch(combo)
            {
                case 0:
                    {
                        if (Combo0.SelectedIndex != -1)
                            return gun[Combo0.SelectedIndex].name;
                        else
                            return "";
                    }
                case 1:
                    {
                        if (Combo1.SelectedIndex != -1)
                            return gun[Combo1.SelectedIndex].name;
                        else
                            return "";
                    }
                case 2:
                    {
                        if (Combo2.SelectedIndex != -1)
                            return gun[Combo2.SelectedIndex].name;
                        else
                            return "";
                    }
                case 3:
                    {
                        if (Combo3.SelectedIndex != -1)
                            return gun[Combo3.SelectedIndex].name;
                        else
                            return "";
                    }
                case 4:
                    {
                        if (Combo4.SelectedIndex != -1)
                            return gun[Combo4.SelectedIndex].name;
                        else
                            return "";
                    }
                case 5:
                    {
                        if (Combo5.SelectedIndex != -1)
                            return gun[Combo5.SelectedIndex].name;
                        else
                            return "";
                    }
                case 6:
                    {
                        if (Combo6.SelectedIndex != -1)
                            return gun[Combo6.SelectedIndex].name;
                        else
                            return "";
                    }
                case 7:
                    {
                        if (Combo7.SelectedIndex != -1)
                            return gun[Combo7.SelectedIndex].name;
                        else
                            return "";
                    }
                case 8:
                    {
                        if (Combo8.SelectedIndex != -1)
                            return gun[Combo8.SelectedIndex].name;
                        else
                            return "";
                    }
                default:
                    return "";
            }
        }


        void calccombo0buff()
        {
         
            gg[0].cleargg();

            int index0 = Combo0.SelectedIndex;
            int index1 = Combo1.SelectedIndex;
            int index3 = Combo3.SelectedIndex;
            int index4 = Combo4.SelectedIndex;

            if (index0 == -1 || index0 == 101)
            {
                Combo0.SelectedIndex = 101;
                renewindex(0);
                return;
            }
            else
            {
                if(index1!=-1)
                {
                    othercombochange(index1, index0, 4, 0);
                }
                if(index3!=-1)
                {
                    othercombochange(index3, index0, 8, 0);
                }
                if(index4!=-1)
                {
                    othercombochange(index4, index0, 7, 0);
                }
                renewindex(0);
                if(rb0.IsChecked == true)
                    calctank(0);
                if (rbf0.IsChecked == true)
                    calcftank(0);
                return;
            }
        }

        void calccombo1buff()
        {
       
            gg[1].cleargg();

            int index0 = Combo0.SelectedIndex;
            int index1 = Combo1.SelectedIndex;
            int index2 = Combo2.SelectedIndex;
            int index3 = Combo3.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index5 = Combo5.SelectedIndex;

            if (index1 == -1 || index1 == 101)
            {
                Combo1.SelectedIndex = 101;
                renewindex(1);
                return;
            }
            else
            {
                if (index0 != -1)
                {
                    othercombochange(index0, index1, 6, 1);
                }
                if (index2 != -1)
                {
                    othercombochange(index2, index1, 4, 1);
                }
                if (index3 != -1)
                {
                    othercombochange(index3, index1, 9, 1);
                }
                if (index4 != -1)
                {
                    othercombochange(index4, index1, 8, 1);
                }
                if (index5 != -1)
                {
                    othercombochange(index5, index1, 7, 1);
                }
                renewindex(1);
                if (rb1.IsChecked == true)
                    calctank(1);
                if (rbf1.IsChecked == true)
                    calcftank(1);
                return;
            }
        }

        void calccombo2buff()
        {
      
            gg[2].cleargg();

            int index2 = Combo2.SelectedIndex;
            int index1 = Combo1.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index5 = Combo5.SelectedIndex;

            if (index2 == -1 || index2 == 101)
            {
                Combo2.SelectedIndex = 101;
                renewindex(2);
                return;
            }
            else
            {
                if (index1 != -1)
                {
                    othercombochange(index1, index2, 6, 2);
                }
                if (index4 != -1)
                {
                    othercombochange(index4, index2, 9, 2);
                }
                if (index5 != -1)
                {
                    othercombochange(index5, index2, 8, 2);
                }
                renewindex(2);
                if (rb2.IsChecked == true)
                    calctank(2);
                if (rbf2.IsChecked == true)
                    calcftank(2);
                return;
            }
        }

        void calccombo3buff()
        {
   
            gg[3].cleargg();

            int index0 = Combo0.SelectedIndex;
            int index1 = Combo1.SelectedIndex;
            int index6 = Combo6.SelectedIndex;
            int index3 = Combo3.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index7 = Combo7.SelectedIndex;

            if (index3 == -1||index3 == 101)
            {
                Combo3.SelectedIndex = 101;
                renewindex(3);
                return;
            }
            else
            {
                if (index0 != -1)
                {
                    othercombochange(index0, index3, 2, 3);
                }
                if (index1 != -1)
                {
                    othercombochange(index1, index3, 1, 3);
                }
                if (index4 != -1)
                {
                    othercombochange(index4, index3, 4, 3);
                }
                if (index6 != -1)
                {
                    othercombochange(index6, index3, 8, 3);
                }
                if (index7 != -1)
                {
                    othercombochange(index7, index3, 7, 3);
                }
                renewindex(3);
                if (rb3.IsChecked == true)
                    calctank(3);
                if (rbf3.IsChecked == true)
                    calcftank(3);
                return;
            }
        }

        void calccombo4buff()
        {

            gg[4].cleargg();

            int index0 = Combo0.SelectedIndex;
            int index1 = Combo1.SelectedIndex;
            int index2 = Combo2.SelectedIndex;
            int index3 = Combo3.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index5 = Combo5.SelectedIndex;
            int index6 = Combo6.SelectedIndex;
            int index7 = Combo7.SelectedIndex;
            int index8 = Combo8.SelectedIndex;

            if (index4 == -1 || index4 == 101)
            {
                Combo4.SelectedIndex = 101;
                renewindex(4);
                return;
            }
            else
            {
                if (index0 != -1)
                {
                    othercombochange(index0, index4, 3, 4);
                }
                if (index1 != -1)
                {
                    othercombochange(index1, index4, 2, 4);
                }
                if (index3 != -1)
                {
                    othercombochange(index3, index4, 6, 4);
                }
                if (index2 != -1)
                {
                    othercombochange(index2, index4, 1, 4);
                }
                if (index5 != -1)
                {
                    othercombochange(index5, index4, 4, 4);
                }
                if (index6 != -1)
                {
                    othercombochange(index6, index4, 9, 4);
                }
                if (index7 != -1)
                {
                    othercombochange(index7, index4, 8, 4);
                }
                if (index8 != -1)
                {
                    othercombochange(index8, index4, 7, 4);
                }
                renewindex(4);
                if (rb4.IsChecked == true)
                    calctank(4);
                if (rbf4.IsChecked == true)
                    calcftank(4);
                return;
            }
        }

        void calccombo5buff()
        {
            gg[5].cleargg();

            int index2 = Combo2.SelectedIndex;
            int index1 = Combo1.SelectedIndex;
            int index8 = Combo8.SelectedIndex;
            int index5 = Combo5.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index7 = Combo7.SelectedIndex;

            if (index5 == -1 || index5 == 101)
            {
                Combo5.SelectedIndex = 101;
                renewindex(5);
                return;
            }
            else
            {
                if (index1 != -1)
                {
                    othercombochange(index1, index5, 3, 5);
                }
                if (index2 != -1)
                {
                    othercombochange(index2, index5, 2, 5);
                }
                if (index4 != -1)
                {
                    othercombochange(index4, index5, 6, 5);
                }
                if (index7 != -1)
                {
                    othercombochange(index7, index5, 9, 5);
                }
                if (index8 != -1)
                {
                    othercombochange(index8, index5, 8, 5);
                }
                renewindex(5);
                if (rb5.IsChecked == true)
                    calctank(5);
                if (rbf5.IsChecked == true)
                    calcftank(5);
                return;
            }
        }

        void calccombo6buff()
        {
            gg[6].cleargg();

            int index6 = Combo6.SelectedIndex;
            int index7 = Combo7.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index3 = Combo3.SelectedIndex;

            if (index6 == -1 || index6 == 101)
            {
                Combo6.SelectedIndex = 101;
                renewindex(6);
                return;
            }
            else
            {
                if (index3 != -1)
                {
                    othercombochange(index3, index6, 2, 6);
                }
                if (index4 != -1)
                {
                    othercombochange(index4, index6, 1, 6);
                }
                if (index7 != -1)
                {
                    othercombochange(index7, index6, 4, 6);
                }
                renewindex(6);
                if (rb6.IsChecked == true)
                    calctank(6);
                if (rbf6.IsChecked == true)
                    calcftank(6);
                return;
            }
        }

        void calccombo7buff()
        {
            gg[7].cleargg();

            int index3 = Combo3.SelectedIndex;
            int index6 = Combo6.SelectedIndex;
            int index8 = Combo8.SelectedIndex;
            int index5 = Combo5.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index7 = Combo7.SelectedIndex;

            if (index7 == -1 || index7 == 101)
            {
                Combo7.SelectedIndex = 101;
                renewindex(7);
                return;
            }
            else
            {
                if (index3 != -1)
                {
                    othercombochange(index3, index7, 3, 7);
                }
                if (index4 != -1)
                {
                    othercombochange(index4, index7, 2, 7);
                }
                if (index5 != -1)
                {
                    othercombochange(index5, index7, 1, 7);
                }
                if (index6 != -1)
                {
                    othercombochange(index6, index7, 6, 7);
                }
                if (index8 != -1)
                {
                    othercombochange(index8, index5, 4, 7);
                }
                renewindex(7);
                if (rb7.IsChecked == true)
                    calctank(7);
                if (rbf7.IsChecked == true)
                    calcftank(7);
                return;
            }
        }

        void calccombo8buff()
        {
            gg[8].cleargg();

            int index5 = Combo5.SelectedIndex;
            int index7 = Combo7.SelectedIndex;
            int index4 = Combo4.SelectedIndex;
            int index8 = Combo8.SelectedIndex;

            if (index8 == -1 || index8 == 101)
            {
                Combo8.SelectedIndex = 101;
                renewindex(8);
                return;
            }
            else
            {
                if (index4 != -1)
                {
                    othercombochange(index4, index8, 3, 8);
                }
                if (index5 != -1)
                {
                    othercombochange(index5, index8, 2, 8);
                }
                if (index7 != -1)
                {
                    othercombochange(index7, index8, 6, 8);
                }
                renewindex(8);
                if (rb8.IsChecked == true)
                    calctank(8);
                if (rbf8.IsChecked == true)
                    calcftank(8);
                return;
            }
        }



        
        private void Combo0_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int select = Combo0.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != ""&&getcombogunname(i)!=null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo0.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo0.SelectedIndex = lastgunindex[0];
                    return;
                }
            if (select!=-1)
                calccombo0buff();
            calccombo1buff();
            calccombo3buff();
            calccombo4buff();
            lastgunindex[0] = select;

        }

        private void Combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo1.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo1.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo1.SelectedIndex = lastgunindex[1];
                    return;
                }
            if (select != -1)
                calccombo1buff();
            calccombo0buff();
            calccombo2buff();
            calccombo3buff();
            calccombo4buff();
            calccombo5buff();
            lastgunindex[1] = select;
        }

        private void Combo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo2.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo2.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo2.SelectedIndex = lastgunindex[2];
                    return;
                }
            if (select != -1)
                calccombo2buff();
            calccombo1buff();
            calccombo5buff();
            calccombo4buff();
            lastgunindex[2] = select;
        }

        private void Combo3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo3.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo3.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo3.SelectedIndex = lastgunindex[3];
                    return;
                }
            if (select != -1)
                calccombo3buff();
            calccombo0buff();
            calccombo6buff();
            calccombo1buff();
            calccombo4buff();
            calccombo7buff();
            lastgunindex[3] = select;
        }

 

        private void Combo4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo4.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo4.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo4.SelectedIndex = lastgunindex[4];
                    return;
                }
            if (select != -1)
                calccombo4buff();
            calccombo0buff();
            calccombo1buff();
            calccombo2buff();
            calccombo3buff();
            calccombo5buff();
            calccombo6buff();
            calccombo7buff();
            calccombo8buff();

            lastgunindex[4] = select;
        }

        private void Combo5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo5.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo5.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo5.SelectedIndex = lastgunindex[5];
                    return;
                }
            if (select != -1)
                calccombo5buff();
            calccombo1buff();
            calccombo2buff();
            calccombo7buff();
            calccombo4buff();
            calccombo8buff();

            lastgunindex[5] = select;
        }

        private void Combo6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo6.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo6.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(7) || gun[select].name == getcombogunname(8))
                {
                    Combo6.SelectedIndex = lastgunindex[6];
                    return;
                }
            if (select != -1)
                calccombo6buff();
            calccombo3buff();
            calccombo4buff();
            calccombo7buff();
            lastgunindex[6] = select;

        }

        private void Combo7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo7.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo7.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1 && select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(8))
                {
                    Combo7.SelectedIndex = lastgunindex[7];
                    return;
                }
            if (select != -1)
                calccombo7buff();
            calccombo3buff();
            calccombo4buff();
            calccombo5buff();
            calccombo6buff();
            calccombo8buff();

            lastgunindex[7] = select;
        }

        private void Combo8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int select = Combo8.SelectedIndex;
            for (int i = 0; i < 9; i++)
            {
                if (getcombogunname(i) != "" && getcombogunname(i) != null)
                    howmany++;
            }

            if (howmany == 6)
            {
                howmany = 0;
                Combo8.SelectedIndex = 101;
                return;
            }
            else
                howmany = 0;
            if (select != -1&&select != 101)
                if (gun[select].name == getcombogunname(0) || gun[select].name == getcombogunname(1) || gun[select].name == getcombogunname(2) || gun[select].name == getcombogunname(3) || gun[select].name == getcombogunname(4) || gun[select].name == getcombogunname(5) || gun[select].name == getcombogunname(6) || gun[select].name == getcombogunname(7))
                {
                    Combo8.SelectedIndex = lastgunindex[8]; 
                    return;
                }
            if (select != -1)
                calccombo8buff();
            calccombo7buff();
            calccombo4buff();
            calccombo5buff();

            lastgunindex[8] = select;
        }

        public bool IsNumber(String strNumber)

        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsNumber(enemydodge.Text))
                enemydodge.Text = "0";
            int select = Combo0.SelectedIndex;
            if(select!=-1)
                renewindex(0);
            select = Combo1.SelectedIndex;
            if (select != -1)
                renewindex(1);
            select = Combo2.SelectedIndex;
            if (select != -1)
                renewindex(2);
            select = Combo3.SelectedIndex;
            if (select != -1)
                renewindex(3);
            select = Combo4.SelectedIndex;
            if (select != -1)
                renewindex(4);
            select = Combo5.SelectedIndex;
            if (select != -1)
                renewindex(5);
            select = Combo6.SelectedIndex;
            if (select != -1)
                renewindex(6);
            select = Combo7.SelectedIndex;
            if (select != -1)
                renewindex(7);
            select = Combo8.SelectedIndex;
            if (select != -1)
                renewindex(8);
         }


        void   calctank(int combo)
        {
            nowhit.Content = Double.Parse(enemyhit.Text) * skilldownhit;  
            switch(combo)
            {
                case 0:
                    {
                        if(Combo0.SelectedIndex!=-1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp0.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge0.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;         
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 1:
                    {
                        if (Combo1.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp1.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge1.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 2:
                    {
                        if (Combo2.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp2.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge2.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 3:
                    {
                        if (Combo3.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp3.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge3.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 4:
                    {
                        if (Combo4.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp4.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge4.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 5:
                    {
                        if (Combo5.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp5.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge5.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 6:
                    {
                        if (Combo6.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp6.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge6.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 7:
                    {
                        if (Combo7.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp7.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge7.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                case 8:
                    {
                        if (Combo8.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                tank.Content = (Double.Parse((Double.Parse(Lhp8.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge8.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                tank.Content = 0;
                        }
                        else
                            tank.Content = 0;
                        break;
                    }
                default:
                    break;

            }
        }

        void calcftank(int combo)
        {
            nowhit.Content = Double.Parse(enemyhit.Text) * skilldownhit;
            switch (combo)
            {
                case 0:
                    {
                        if (Combo0.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp0.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge0.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 1:
                    {
                        if (Combo1.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp1.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge1.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 2:
                    {
                        if (Combo2.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp2.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge2.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 3:
                    {
                        if (Combo3.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp3.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge3.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 4:
                    {
                        if (Combo4.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp4.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge4.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 5:
                    {
                        if (Combo5.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp5.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge5.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 6:
                    {
                        if (Combo6.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp6.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge6.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 7:
                    {
                        if (Combo7.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp7.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge7.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                case 8:
                    {
                        if (Combo8.SelectedIndex != -1)
                        {
                            if (Int32.Parse(nowhit.Content.ToString()) != 0)
                                ftank.Content = (Double.Parse((Double.Parse(Lhp8.Content.ToString()) / (1 / (1 + Double.Parse(Ldodge8.Content.ToString()) / Int32.Parse(nowhit.Content.ToString())))).ToString())).ToString("0.00");
                            else
                                ftank.Content = 0;
                        }
                        else
                            ftank.Content = 0;
                        break;
                    }
                default:
                    break;

            }
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsNumber(enemyhit.Text))
                enemyhit.Text = "0";
            if (rb0.IsChecked == true)
            {
                calctank(0);
            }
            else if(rb1.IsChecked == true)
            {
                calctank(1);
            }
            else if(rb2.IsChecked == true)
            {
                calctank(2);
            }
            else if (rb3.IsChecked == true)
            {
                calctank(3);
            }
            else if (rb4.IsChecked == true)
            {
                calctank(4);
            }
            else if (rb5.IsChecked == true)
            {
                calctank(5);
            }
            else if (rb6.IsChecked == true)
            {
                calctank(6);
            }
            else if (rb7.IsChecked == true)
            {
                calctank(7);
            }
            else if (rb7.IsChecked == true)
            {
                calctank(7);
            }
            else if (rb8.IsChecked == true)
            {
                calctank(8);
            }
            if (rbf0.IsChecked == true)
            {
                calcftank(0);
            }
            else if (rbf1.IsChecked == true)
            {
                calcftank(1);
            }
            else if (rbf2.IsChecked == true)
            {
                calcftank(2);
            }
            else if (rbf3.IsChecked == true)
            {
                calcftank(3);
            }
            else if (rbf4.IsChecked == true)
            {
                calcftank(4);
            }
            else if (rbf5.IsChecked == true)
            {
                calcftank(5);
            }
            else if (rbf6.IsChecked == true)
            {
                calcftank(6);
            }
            else if (rbf7.IsChecked == true)
            {
                calcftank(7);
            }
            else if (rbf7.IsChecked == true)
            {
                calcftank(7);
            }
            else if (rbf8.IsChecked == true)
            {
                calcftank(8);
            }
        }

        private void rb0_Checked(object sender, RoutedEventArgs e)
        {
            calctank(0);
        }
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            calctank(1);
        }
        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            calctank(2);
        }
        private void rb3_Checked(object sender, RoutedEventArgs e)
        {
            calctank(3);
        }
        private void rb4_Checked(object sender, RoutedEventArgs e)
        {
            calctank(4);
        }
        private void rb5_Checked(object sender, RoutedEventArgs e)
        {
            calctank(5);
        }
        private void rb6_Checked(object sender, RoutedEventArgs e)
        {
            calctank(6);
        }
        private void rb7_Checked(object sender, RoutedEventArgs e)
        {
            calctank(7);
        }
        private void rb8_Checked(object sender, RoutedEventArgs e)
        {
            calctank(8);
        }

        private void reinit_Click(object sender, RoutedEventArgs e)
        {
            bignews.Visibility = Visibility.Visible;
            for (int i = 0; i < 9; i++)
            {
                gg[i] = new GunGrid();
                gg[i].critup = 1.00;
                gg[i].damageup = 1.00;
                gg[i].dodgeup = 1.00;
                gg[i].hitup = 1.00;
                gg[i].shotspeedup = 1.00;
                lastgunindex[i] = -1;
                gg[i].rateup = 0;
            }
            howmany = 0;

            Combo0.SelectedIndex = 101;
            renewindex(0);

     //       Combo0.SelectedIndex = -1;
            Combo1.SelectedIndex = 101;
            renewindex(1);
     //       Combo1.SelectedIndex = -1;
            Combo2.SelectedIndex = 101;
            renewindex(2);
      //      Combo2.SelectedIndex = -1;
            Combo3.SelectedIndex = 101;
            renewindex(3);
    //        Combo3.SelectedIndex = -1;
            Combo4.SelectedIndex = 101;
            renewindex(4);
     //       Combo4.SelectedIndex = -1;
            Combo5.SelectedIndex = 101;
            renewindex(5);
    //        Combo5.SelectedIndex = -1;
            Combo6.SelectedIndex = 101;
            renewindex(6);
     //       Combo6.SelectedIndex = -1;
            Combo7.SelectedIndex = 101;
            renewindex(7);
    //        Combo7.SelectedIndex = -1;
            Combo8.SelectedIndex = 101;
            renewindex(8);
    //        Combo8.SelectedIndex = -1;

            rb0.IsChecked = false;
            rb1.IsChecked = false;
            rb2.IsChecked = false;
            rb3.IsChecked = false;
            rb4.IsChecked = false;
            rb5.IsChecked = false;
            rb6.IsChecked = false;
            rb7.IsChecked = false;
            rb8.IsChecked = false;

            tank.Content = 0;
            enemydodge.Text = "0";
            enemyhit.Text = "0";

            rbf0.IsChecked = false;
            rbf1.IsChecked = false;
            rbf2.IsChecked = false;
            rbf3.IsChecked = false;
            rbf4.IsChecked = false;
            rbf5.IsChecked = false;
            rbf6.IsChecked = false;
            rbf7.IsChecked = false;
            rbf8.IsChecked = false;

            ftank.Content = 0;

            cb0.IsChecked = false;
            cb1.IsChecked = false;
            cb2.IsChecked = false;
            cb3.IsChecked = false;
            cb4.IsChecked = false;
            cb5.IsChecked = false;
            cb6.IsChecked = false;
            cb7.IsChecked = false;
            cb8.IsChecked = false;

            Lskillrate0.Content = "0%";
            Lskillrate1.Content = "0%";
            Lskillrate2.Content = "0%";
            Lskillrate3.Content = "0%";
            Lskillrate4.Content = "0%";
            Lskillrate5.Content = "0%";
            Lskillrate6.Content = "0%";
            Lskillrate7.Content = "0%";
            Lskillrate8.Content = "0%";

        }

        private void rbf0_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(0);
        }
        private void rbf1_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(1);
        }
        private void rbf2_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(2);
        }
        private void rbf3_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(3);
        }
        private void rbf4_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(4);
        }
        private void rbf5_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(5);
        }
        private void rbf6_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(6);
        }
        private void rbf7_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(7);
        }
        private void rbf8_Checked(object sender, RoutedEventArgs e)
        {
            calcftank(8);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Summary s = new Summary();
            s.ShowDialog();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            UpdateData u = new UpdateData();
            u.ShowDialog();
        }

        private void skillon(int index, int combo)
        {
            if (index != -1 && index != 101)
            {
                switch (gun[index].skilltype)
                {

                    //   skilltype : 4 upalldamage 6 upmydamage 8downallhit 9upmyhit 18 upmyshotspeed 19 烟雾弹 21 upalldodge 22 downalldodge 23 upmydodge 24 upallhit 26 upallshotspeed
                    case 4:
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                skillupdamage[i] += gun[index].skillupalldamage;
                                renewindex(i);
                            }
                            return;
                        }
                    case 6:
                        {
                            skillupdamage[combo] += gun[index].skillupmydamage;
                            renewindex(combo);
                            return;
                        }
                    case 8:
                        {
                            skilldownhit -= gun[index].skilldownallenemyhit;
                            //重置----
                            return;
                        }
                    case 9:
                        {
                            skilluphit[combo] += gun[index].skillupmyhit;
                            renewindex(combo);
                            return;
                        }
                    case 18:
                        {
                            skillupshotspeed[combo] += gun[index].skillupmyshotspeed;
                            renewindex(combo);
                            return;
                        }
                    case 19:
                        {
                            skilldownhit -= gun[index].skilldownallenemyhit;
                            for (int i = 0; i < 9; i++)
                            {
                                renewindex(i);
                            }
                            return;
                        }
                    case 21:
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                skillupdodge[i] += gun[index].skillupalldodge;
                                renewindex(i);
                            }
                            return;
                        }
                    case 22:
                        {
                            skilldowndodge -= gun[index].skilldownallenemydodge;
                                if (rb0.IsChecked == true)
            {
                calctank(0);
            }
            else if(rb1.IsChecked == true)
            {
                calctank(1);
            }
            else if(rb2.IsChecked == true)
            {
                calctank(2);
            }
            else if (rb3.IsChecked == true)
            {
                calctank(3);
            }
            else if (rb4.IsChecked == true)
            {
                calctank(4);
            }
            else if (rb5.IsChecked == true)
            {
                calctank(5);
            }
            else if (rb6.IsChecked == true)
            {
                calctank(6);
            }
            else if (rb7.IsChecked == true)
            {
                calctank(7);
            }
            else if (rb7.IsChecked == true)
            {
                calctank(7);
            }
            else if (rb8.IsChecked == true)
            {
                calctank(8);
            }
            if (rbf0.IsChecked == true)
            {
                calcftank(0);
            }
            else if (rbf1.IsChecked == true)
            {
                calcftank(1);
            }
            else if (rbf2.IsChecked == true)
            {
                calcftank(2);
            }
            else if (rbf3.IsChecked == true)
            {
                calcftank(3);
            }
            else if (rbf4.IsChecked == true)
            {
                calcftank(4);
            }
            else if (rbf5.IsChecked == true)
            {
                calcftank(5);
            }
            else if (rbf6.IsChecked == true)
            {
                calcftank(6);
            }
            else if (rbf7.IsChecked == true)
            {
                calcftank(7);
            }
            else if (rbf7.IsChecked == true)
            {
                calcftank(7);
            }
            else if (rbf8.IsChecked == true)
            {
                calcftank(8);
            }
                            return;
                        }
                    case 23:
                        {
                            skillupdodge[combo] += gun[index].skillupmydodge;
                            renewindex(combo);
                            return;
                        }
                    case 24:
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                skilluphit[i] += gun[index].skillupallhit;
                                renewindex(i);
                            }
                            return;
                        }
                    case 26:
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                skillupshotspeed[i] += gun[index].skillupallshotspeed;
                                renewindex(i);
                            }
                            return;
                        }



                    default:
                        return;
                }
            }
        }


        private void cb0_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo0.SelectedIndex, 0);
        }
        private void cb1_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo1.SelectedIndex, 1);
        }
        private void cb2_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo2.SelectedIndex, 2);
        }
        private void cb3_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo3.SelectedIndex, 3);
        }
        private void cb4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(cb4.IsChecked.ToString());
            skillon(Combo4.SelectedIndex, 4);
        }
        private void cb5_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo5.SelectedIndex, 5);
        }
        private void cb6_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo6.SelectedIndex, 6);
        }
        private void cb7_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo7.SelectedIndex, 7);
        }
        private void cb8_Click(object sender, RoutedEventArgs e)
        {
            skillon(Combo8.SelectedIndex, 8);
        }
    }
}
