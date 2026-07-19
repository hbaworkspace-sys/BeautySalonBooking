namespace BeautySalonBooking.Maui.Helper
{
    //public class BanksConvertor
    //{
    //    public static string GetBankImage(string bankName)
    //    {
    //        if (bankName == null) return "";
    //        if (bankName.Contains("ملت"))
    //            return @"melat.png";
    //        else if (bankName.Contains("ملی"))
    //            return @"meli.png";
    //        else if (bankName.Contains("تجارت"))
    //            return @"tejarat.png";
    //        else if (bankName.Contains("سپه"))
    //            return @"sepah.png";
    //        else if (bankName.Contains("صادرات"))
    //            return @"saderat.png";
    //        else
    //            return @"melat.png";
    //    }
    //    public static Bank GetBankByCardNumber(string CardNumber, List<Bank> banks)
    //    {
    //        string str = "";
    //        try
    //        {
    //            str = CardNumber.Substring(0, 6);
    //        }
    //        catch { }
    //        if (str == "") return null;
    //        switch (str)
    //        {
    //            case "627412":
    //                return banks.Where(e => e.BankName.Contains("اقتصاد")).FirstOrDefault();
    //            case "603799":
    //                return banks.Where(e => e.BankName.Contains("ملی")).FirstOrDefault();
    //            case "505801":
    //                return banks.Where(e => e.BankName.Contains("کوثر")).FirstOrDefault();
    //            case "628157":
    //                return banks.Where(e => e.BankName.Contains("اعتباری توسعه")).FirstOrDefault();
    //            case "991975":
    //                return banks.Where(e => e.BankName.Contains("ملت")).FirstOrDefault();
    //            case "610433":
    //                return banks.Where(e => e.BankName.Contains("ملت")).FirstOrDefault();
    //            case "628023":
    //                return banks.Where(e => e.BankName.Contains("مسکن")).FirstOrDefault();
    //            case "636795":
    //                return banks.Where(e => e.BankName.Contains("مرکزی")).FirstOrDefault();
    //            case "505416":
    //                return banks.Where(e => e.BankName.Contains("گردشگری")).FirstOrDefault();
    //            case "639217":
    //                return banks.Where(e => e.BankName.Contains("کشاورزی")).FirstOrDefault();
    //            case "603770":
    //                return banks.Where(e => e.BankName.Contains("کشاورزی")).FirstOrDefault();
    //            case "627488":
    //                return banks.Where(e => e.BankName.Contains("آفرین")).FirstOrDefault();
    //            case "502910":
    //                return banks.Where(e => e.BankName.Contains("آفرین")).FirstOrDefault();
    //            case "639599":
    //                return banks.Where(e => e.BankName.Contains("قوامین")).FirstOrDefault();
    //            case "606373":
    //                return banks.Where(e => e.BankName.Contains("مهر")).FirstOrDefault();
    //            case "627961":
    //                return banks.Where(e => e.BankName.Contains("معدن")).FirstOrDefault();
    //            case "603769":
    //                return banks.Where(e => e.BankName.Contains("صادرات")).FirstOrDefault();
    //            case "502806":
    //                return banks.Where(e => e.BankName.Contains("شهر")).FirstOrDefault();
    //            case "639346":
    //                return banks.Where(e => e.BankName.Contains("سینا")).FirstOrDefault();
    //            case "639607":
    //                return banks.Where(e => e.BankName.Contains("سرمایه")).FirstOrDefault();
    //            case "589210":
    //                return banks.Where(e => e.BankName.Contains("سپه")).FirstOrDefault();
    //            case "621986":
    //                return banks.Where(e => e.BankName.Contains("سامان")).FirstOrDefault();
    //            case "589463":
    //                return banks.Where(e => e.BankName.Contains("رفاه")).FirstOrDefault();
    //            case "509238":
    //                return banks.Where(e => e.BankName.Contains("دی")).FirstOrDefault();
    //            case "636949":
    //                return banks.Where(e => e.BankName.Contains("حکمت ایرانیان")).FirstOrDefault();
    //            case "502908":
    //                return banks.Where(e => e.BankName.Contains("تعاون")).FirstOrDefault();
    //            case "627353":
    //                return banks.Where(e => e.BankName.Contains("تجارت")).FirstOrDefault();
    //            case "585983":
    //                return banks.Where(e => e.BankName.Contains("تجارت")).FirstOrDefault();
    //            case "639347":
    //                return banks.Where(e => e.BankName.Contains("پاسارگارد")).FirstOrDefault();
    //            case "502229":
    //                return banks.Where(e => e.BankName.Contains("پاسارگارد")).FirstOrDefault();
    //            case "627884":
    //                return banks.Where(e => e.BankName.Contains("پارسیان")).FirstOrDefault();
    //            case "622106":
    //                return banks.Where(e => e.BankName.Contains("پارسیان")).FirstOrDefault();
    //            case "505785":
    //                return banks.Where(e => e.BankName.Contains("زمین")).FirstOrDefault();
    //            case "627381":
    //                return banks.Where(e => e.BankName.Contains("انصار")).FirstOrDefault();

    //            default:
    //                return null;
    //        }
    //    }
    //    public static string GetBankNameByCardNumber(string CardNumber)
    //    {
    //        string str = "";
    //        try
    //        {
    //            str = CardNumber.Substring(0, 6)
    //                ;
    //        }
    //        catch { }
    //        if (str == "") return null;
    //        switch (str)
    //        {
    //            case "627412":
    //                return "اقتصاد";
    //            case "603799":
    //                return "ملی";
    //            case "505801":
    //                return "کوثر";
    //            case "628157":
    //                return "اعتباری توسعه";
    //            case "991975":
    //                return "ملت";
    //            case "610433":
    //                return "ملت";
    //            case "628023":
    //                return "مسکن";
    //            case "636795":
    //                return "مرکزی";
    //            case "505416":
    //                return "گردشگری";
    //            case "639217":
    //                return "کشاورزی";
    //            case "603770":
    //                return "کشاورزی";
    //            case "627488":
    //                return "آفرین";
    //            case "502910":
    //                return "آفرین";
    //            case "639599":
    //                return "قوامین";
    //            case "606373":
    //                return "مهر";
    //            case "627961":
    //                return "معدن";
    //            case "603769":
    //                return "صادرات";
    //            case "502806":
    //                return "شهر";
    //            case "639346":
    //                return "سینا";
    //            case "639607":
    //                return "سرمایه";
    //            case "589210":
    //                return "سپه";
    //            case "621986":
    //                return "سامان";
    //            case "589463":
    //                return "رفاه";
    //            case "509238":
    //                return "دی";
    //            case "636949":
    //                return "حکمت ایرانیان";
    //            case "502908":
    //                return "تعاون";
    //            case "627353":
    //                return "تجارت";
    //            case "585983":
    //                return "تجارت";
    //            case "639347":
    //                return "پاسارگارد";
    //            case "502229":
    //                return "پاسارگارد";
    //            case "627884":
    //                return "پارسیان";
    //            case "622106":
    //                return "پارسیان";
    //            case "505785":
    //                return "زمین";
    //            case "627381":
    //                return "انصار";

    //            default:
    //                return null;
    //        }
    //    }
    //}
    public class DigitConvertor
    {
        public string ToPersian(string En)
        {
            if (String.IsNullOrEmpty(En))
                return "";

            string fa = En;
            string[] faNums = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            for (int i = 0; i < 10; i++)
            {
                fa = fa.Replace(i.ToString(), faNums[i]);
            }
            return fa;
        }

        public string ToEnglish(string Fa)
        {
            if (String.IsNullOrEmpty(Fa))
                return "";

            string fa = Fa;
            for (int i = 0; i < 10; i++)
            {
                fa = fa.Replace(ToPersian(i.ToString()), i.ToString());
            }
            return fa;
        }

        public bool IsNumeric(string text)
        {
            if (decimal.TryParse(text, out decimal num) == true)
                return true;
            else
                return false;
        }
        public bool IsNumeric_ulong(string text)
        {
            if (ulong.TryParse(text, out ulong num) == true)
                return true;
            else
                return false;
        }
        // ٫

        /// <summary>
        /// seperate Input string with , seperator
        /// </summary>
        /// <param name="str">money as string</param>
        /// <returns>seperated money</returns>
        public static string ToMoneyFormat(string str)
        {
            string txt = "0";
            int dotsCount = str.Count(p => p == '.');
            if (dotsCount > 0)
            {
                if (str.Length == 1 || dotsCount > 1)
                    str = "0";
                else
                    str = Convert.ToDecimal(str).ToString("#");
            }

            if (str == string.Empty)
            {
                txt = "0";
            }
            else if (str == "00")
            {
                txt = "0";
            }
            else if (str != "0")
            {
                txt = Convert.ToDecimal(str).ToString("#,#");
            }
            return txt;
        }
        /// <summary>
        /// تبدیل عدد به حروف
        /// </summary>
        /// <param name="GetAdad">مبلغ به عدد</param>
        /// <returns></returns>
        public static string ToLetter(string GetAdad, string CurrencyType = "تومان")
        {
            string ss = "";
            GetAdad = GetAdad.Replace("-", "");
            try
            {
                Int64 n = Convert.ToInt64(GetAdad);
                Int64 n1 = n;
                Int64 n2 = n;
                Int64 n3 = 0;
                Int64 nn = 1;
                Int64 t = 0;
                Int64 n11 = 0;
                Int64 n22 = 0;
                Int64 n33 = 0;
                string s = "";
                while (n1 != 0)
                {
                    n1 = Convert.ToInt64(n1 / 1000);
                    t = Convert.ToInt64(t + 1);
                }
                t = Convert.ToInt16(t - 1);
                while (t >= 0)
                {
                    nn = 1;
                    for (Int64 i = 1; i <= t; i++)
                        nn = Convert.ToInt64(nn * 1000);
                    n3 = Convert.ToInt64(n2 / nn);
                    n2 = Convert.ToInt64(n2 % nn);
                    n11 = 0;
                    n22 = 0;
                    n33 = 0;
                    if ((n3 >= 1) && (n3 <= 9))
                    {
                        if ((s != "") && (t >= 0))
                            s = s + " و ";
                        n33 = n3;
                        switch (n33)
                        {
                            case 1:
                                s = s + "یک";
                                break;
                            case 2:
                                s = s + "دو";
                                break;
                            case 3:
                                s = s + "سه";
                                break;
                            case 4:
                                s = s + "چهار";
                                break;
                            case 5:
                                s = s + "پنج";
                                break;
                            case 6:
                                s = s + "شش";
                                break;
                            case 7:
                                s = s + "هفت";
                                break;
                            case 8:
                                s = s + "هشت";
                                break;
                            case 9:
                                s = s + "نه";
                                break;
                        }
                    }
                    if ((n3 >= 10) && (n3 <= 99))
                    {
                        if ((s != "") && (t >= 0))
                            s = s + " و ";
                        n22 = Convert.ToInt64(n3 / 10);
                        n3 = Convert.ToInt64(n3 % 10);
                        n33 = n3;
                        switch (n22)
                        {
                            case 1:
                                if (n33 == 0)
                                    s = s + "ده";
                                if (n33 == 1)
                                    s = s + "یازده";
                                if (n33 == 2)
                                    s = s + "دوازده";
                                if (n33 == 3)
                                    s = s + "سیزده";
                                if (n33 == 4)
                                    s = s + "چهارده";
                                if (n33 == 5)
                                    s = s + "پانزده";
                                if (n33 == 6)
                                    s = s + "شانزده";
                                if (n33 == 7)
                                    s = s + "هفده";
                                if (n33 == 8)
                                    s = s + "هجده";
                                if (n33 == 9)
                                    s = s + "نوزده";
                                break;
                            case 2:
                                s = s + "بیست";
                                break;
                            case 3:
                                s = s + "سی";
                                break;
                            case 4:
                                s = s + "چهل";
                                break;
                            case 5:
                                s = s + "پنجاه";
                                break;
                            case 6:
                                s = s + "شصت";
                                break;
                            case 7:
                                s = s + "هفتاد";
                                break;
                            case 8:
                                s = s + "هشتاد";
                                break;
                            case 9:
                                s = s + "نود";
                                break;
                        }
                        if ((n33 != 0) && (n22 != 1))
                            s = s + " و ";
                        if (n22 != 1)
                        {
                            switch (n33)
                            {
                                case 1:
                                    s = s + "یک";
                                    break;
                                case 2:
                                    s = s + "دو";
                                    break;
                                case 3:
                                    s = s + "سه";
                                    break;
                                case 4:
                                    s = s + "چهار";
                                    break;
                                case 5:
                                    s = s + "پنج";
                                    break;
                                case 6:
                                    s = s + "شش";
                                    break;
                                case 7:
                                    s = s + "هفت";
                                    break;
                                case 8:
                                    s = s + "هشت";
                                    break;
                                case 9:
                                    s = s + "نه";
                                    break;
                            }
                        }
                    }
                    if ((n3 >= 100) && (n3 <= 999))
                    {
                        if ((s != "") && (t >= 0))
                            s = s + " و ";
                        n11 = Convert.ToInt64(n3 / 100);
                        n3 = Convert.ToInt64(n3 % 100);
                        n22 = Convert.ToInt64(n3 / 10);
                        n3 = Convert.ToInt64(n3 % 10);
                        n33 = n3;
                        switch (n11)
                        {
                            case 1:
                                s = s + "یکصد";
                                break;
                            case 2:
                                s = s + "دویست";
                                break;
                            case 3:
                                s = s + "سیصد";
                                break;
                            case 4:
                                s = s + "چهارصد";
                                break;
                            case 5:
                                s = s + "پانصد";
                                break;
                            case 6:
                                s = s + "ششصد";
                                break;
                            case 7:
                                s = s + "هفتصد";
                                break;
                            case 8:
                                s = s + "هشتصد";
                                break;
                            case 9:
                                s = s + "نهصد";
                                break;
                        }
                        if (n22 != 0)
                            s = s + " و ";
                        switch (n22)
                        {
                            case 1:
                                if (n33 == 0)
                                    s = s + "ده";
                                if (n33 == 1)
                                    s = s + "یازده";
                                if (n33 == 2)
                                    s = s + "دوازده";
                                if (n33 == 3)
                                    s = s + "سیزده";
                                if (n33 == 4)
                                    s = s + "چهارده";
                                if (n33 == 5)
                                    s = s + "پانزده";
                                if (n33 == 6)
                                    s = s + "شانزده";
                                if (n33 == 7)
                                    s = s + "هفده";
                                if (n33 == 8)
                                    s = s + "هجده";
                                if (n33 == 9)
                                    s = s + "نوزده";
                                break;
                            case 2:
                                s = s + "بیست";
                                break;
                            case 3:
                                s = s + "سی";
                                break;
                            case 4:
                                s = s + "چهل";
                                break;
                            case 5:
                                s = s + "پنجاه";
                                break;
                            case 6:
                                s = s + "شصت";
                                break;
                            case 7:
                                s = s + "هفتاد";
                                break;
                            case 8:
                                s = s + "هشتاد";
                                break;
                            case 9:
                                s = s + "نود";
                                break;
                        }
                        if ((n33 != 0) && (n22 != 1))
                            s = s + " و ";
                        if (n22 != 1)
                        {
                            switch (n33)
                            {
                                case 1:
                                    s = s + "یک";
                                    break;
                                case 2:
                                    s = s + "دو";
                                    break;
                                case 3:
                                    s = s + "سه";
                                    break;
                                case 4:
                                    s = s + "چهار";
                                    break;
                                case 5:
                                    s = s + "پنج";
                                    break;
                                case 6:
                                    s = s + "شش";
                                    break;
                                case 7:
                                    s = s + "هفت";
                                    break;
                                case 8:
                                    s = s + "هشت";
                                    break;
                                case 9:
                                    s = s + "نه";
                                    break;
                            }
                        }
                    }
                    if ((t == 4) && ((n11 != 0) || (n22 != 0) || (n33 != 0)))
                        s = s + " تریلیون ";
                    if ((t == 3) && ((n11 != 0) || (n22 != 0) || (n33 != 0)))
                        s = s + " میلیارد ";
                    if ((t == 2) && ((n11 != 0) || (n22 != 0) || (n33 != 0)))
                        s = s + " میلیون ";
                    if ((t == 1) && ((n11 != 0) || (n22 != 0) || (n33 != 0)))
                        s = s + " هزار ";
                    t = Convert.ToInt16(t - 1);
                }

                if (n == 0)
                {
                    ss = "صفر";
                    //  Label 1.Text = "صفر";
                }
                else
                {
                    // Label 1.Text = s;
                    ss = s;
                }
            }
            catch
            {
            }
            return ss + " " + CurrencyType;



        }

        public static bool CarNumberValidator(string carNo)
        {
            bool secuss = true;
            //اگر رقم اول عدد بود
            try
            {
                if (int.TryParse(carNo[0].ToString(), out int q) == false)
                    secuss = false;
            }
            catch
            {
            }
            //اگر رقم دوم عدد بود
            try
            {
                if (int.TryParse(carNo[1].ToString(), out int q) == false)
                    secuss = false;
            }
            catch
            {

            }

            //اگر رقم سوم حروف بود
            try
            {
                if (int.TryParse(carNo[2].ToString(), out int q) == true)
                    secuss = false;
            }
            catch
            {

            }
            //اگر رقم چهارم عدد بود
            try
            {
                if (int.TryParse(carNo[3].ToString(), out int q) == false)
                    secuss = false;
            }
            catch
            {

            }
            //اگر رقم پنجم عدد بود
            try
            {
                if (int.TryParse(carNo[4].ToString(), out int q) == false)
                    secuss = false;
            }
            catch
            {

            }
            return secuss;
        }

        //public static decimal CalculateRounding(string cash)
        //{
        //    //cash = cash.Replace(",", "");
        //    ////روند شدن روی مثلا 500 تومان
        //    //decimal RRange = AccountHelper.RoundingRange;
        //    ////گرفتن سه رقم آخر
        //    //decimal Change = decimal.Parse(cash.Substring(cash.Length - 3, 3));
        //    //decimal x = 0;
        //    ////محاسبه رندینگ
        //    //if (Change < RRange)
        //    //{
        //    //    x = Change;
        //    //}
        //    //else if (Change >= RRange)
        //    //{
        //    //    x = -((RRange * 2) - Change);
        //    //}
        //    //return x;
        //}

    }

}