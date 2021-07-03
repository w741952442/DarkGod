/****************************************************
    文件：Contents.cs
	作者：Mouse
    日期：2021/6/18 17:44:58
	功能：工具类
*****************************************************/
public class PETools
{
    //定义静态的随机数函数接口
    //param
    //@min：最小值
    //@max：最大值
    //@random：随机数体
    public static int RandomInt(int min, int max, System.Random random = null)
    {
        if (random == null)
        {
            random = new System.Random();
        }
        int val = random.Next(min, max + 1);
        return val;
    }
}