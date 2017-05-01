using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;
using Models.Enums;

namespace Models
{
    public class Card
    {
        public int CardValue { get;}

        public BitmapImage BitmapImage { get;
            //set { _bitmapImage = value; }
        }


        public Card(int cardValue, CardType cardType)
        {
            if (cardValue > 10 && cardValue < 14)
            {
                CardValue = 10;
            }else if (cardValue == 14)
            {
                CardValue = 11;
            }
            else
            {
                CardValue = cardValue;
            }
            string path;
            string folder = "";
            switch (cardType)
            {
                    case CardType.Clubs:
                        folder = "Clubs";
                        break;
                    case CardType.Diamonds:
                        folder = "Diamonds";
                        break;
                    case CardType.Hearts:
                        folder = "Hearts";
                        break;
                    case CardType.Spades:
                        folder = "Spades";
                        break;
            }
            try
            {
#if DEBUG
                DirectoryInfo directoryInfo =
                    Directory.GetParent(path: Directory.GetParent(path: Environment.CurrentDirectory).FullName);
                path = directoryInfo.FullName + @"\Images\Cards\" + folder + "\\" + cardValue + ".png";
#endif
#if (!DEBUG)
                path = Environment.CurrentDirectory + @"\Images\Cards\" + folder + "\\" + cardValue + ".png";
#endif
            }
            catch (Exception e)
            {
                throw new Exception(message: "Unable to find card image files");
            }

            BitmapImage =new BitmapImage(uriSource: new Uri(uriString: path));
            
        }

        
    }
}
