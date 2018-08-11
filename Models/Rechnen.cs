using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rechentrainer.Models
{
    public class RechnenClass
    {
        public List<EinzelRechnung> Rechnungen { get; set; }
        public Zeit RechenZeit { get; set; }
        public Ergebnis Ergebnis { get; set; }
        public bool ShowResult { get; set; }

        public RechnenClass()
        {
            Rechnungen = new List<EinzelRechnung>();
            for (int i = 0; i < 14; i++)
            {
                Rechnungen.Add(new EinzelRechnung());
            }

            RechenZeit = new Zeit();
            Ergebnis = new Ergebnis();
        }

        public int GibMirPunkte()
        {
            int punkte = 0;
            foreach (EinzelRechnung er in Rechnungen)
            {
                punkte += er.GibMirPunkt();
            }

            return punkte;
        }

        public decimal GibMirGeschwindigkeit()
        {
            return RechenZeit.GetSpeed();
        }
    }

    public class EinzelRechnung
    {
        public int ErsterWert { get; set; }
        public int ZweiterWert { get; set; }
        public int? Ergebnis { get; set; }
        public eRechnungsTyp Typ { get; set; }
        public int RichtigesErgebnis { get; set; }

        public EinzelRechnung(int zahl)
        {
            switch (zahl)
            {
                case 1:
                    ErsterWert = 7;
                    ZweiterWert = 8;
                    RichtigesErgebnis = ErsterWert + ZweiterWert;
                    break;
                case 2:
                    ErsterWert = 3;
                    ZweiterWert = 5;
                    RichtigesErgebnis = ErsterWert + ZweiterWert;
                    break;
                default:
                    ErsterWert = 1;
                    ZweiterWert = 7;
                    RichtigesErgebnis = ErsterWert + ZweiterWert;
                    break;
            }
        }
        public EinzelRechnung()
        {
            SetPlus(100, true);
        }

        private void SetPlus(int max, bool noTen)
        {
            ErsterWert = RandomFast.RandomNumber(max > 10 ? 0 : 1, max);
            ZweiterWert = RandomFast.RandomNumber(0, max - ErsterWert);
            if (noTen) 
            {
                int[] minFor2nd = new int[] { RandomFast.RandomNumber(0, 10), (100 - ErsterWert) };
                ZweiterWert = minFor2nd.Min();
            }
            Typ = eRechnungsTyp.Plus;
            Ergebnis = null;
            RichtigesErgebnis = ErsterWert + ZweiterWert;
        }

        public int GibMirPunkt()
        {
            return RichtigesErgebnis == Ergebnis ? 1 : 0;
        }
    }

    public class Zeit
    {
        public decimal Dauer { get; set; }

        public Zeit()
        {
            Dauer = 0;
        }

        public decimal GetSpeed()
        {
            decimal speed = 14 / Dauer * 60;
            return Math.Round(speed, 1);
        }
    }

    public class Ergebnis
    {
        public int MaximalPunkte { get; private set; }
        public int Punkte { get; private set; }
        public decimal Geschwindigkeit { get; private set; }
        public int Fehler { get { return MaximalPunkte - Punkte; } }
        public decimal Dauer { get; private set; }

        public Ergebnis() { MaximalPunkte = 14; }

        public void Update(int punkte, decimal geschwindigkeit)
        {
            Punkte = punkte;
            Geschwindigkeit = geschwindigkeit;
            Dauer = Math.Round((60 * 14 / Geschwindigkeit), 1);
        }
    }

    public static class RandomFast
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            if (min > max) { min = max; }
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }

    public enum eRechnungsTyp : byte
    {
        Plus = 0,
        Minus = 1
    }
}
