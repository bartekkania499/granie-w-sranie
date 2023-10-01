using System;

namespace GraRPG
{
    class Postac
    {
        public string Nazwa { get; protected set; }
        public int HP { get; protected set; }
        public int Atak { get; protected set; }
        public int Mana { get; protected set; }
        public int Zloto { get; protected set; }

        public Postac(string nazwa, int hp, int atak, int mana, int zloto)
        {
            Nazwa = nazwa;
            HP = hp;
            Atak = atak;
            Mana = mana;
            Zloto = zloto;
        }

        public virtual void Informacje()
        {
            Console.WriteLine($"{Nazwa}:\nHP - {HP}\nAtak - {Atak}\nMana - {Mana}\nZłoto - {Zloto}");
        }

        public void WypijMiksture()
        {
            if (Mana >= 20)
            {
                HP += 15;
                Mana -= 20;
                Console.WriteLine("Użyłeś mikstury (+15 HP, -20 many)");
            }
            else
            {
                Console.WriteLine("Brak many");
            }
        }

        public void KupMiksture()
        {
            if (Zloto >= 5)
            {
                Zloto -= 5;
                HP += 25;
                Console.WriteLine("Kupiłeś miksturę (+25 HP)");
            }
            else
            {
                Console.WriteLine("Nie masz wystarczająco złota");
            }
        }

        public void KupBron()
        {
            if (Zloto >= 15)
            {
                Zloto -= 15;
                Atak += 20;
                Console.WriteLine("Kupiłeś broń (+20 ataku)");
            }
            else
            {
                Console.WriteLine("Nie masz wystarczająco złota");
            }
        }

        public void KupMany()
        {
            if (Zloto >= 8)
            {
                Zloto -= 8;
                Mana += 50;
                Console.WriteLine("Kupiłeś many (+50 many)");
            }
            else
            {
                Console.WriteLine("Nie masz wystarczająco złota");
            }
        }
    }

    class Wojownik : Postac
    {
        public Wojownik() : base("Wojownik", 200, 60, 0, 15) { }
    }

    class Mag : Postac
    {
        public Mag() : base("Mag", 150, 30, 150, 20) { }

        public void RzucCzar()
        {
            if (Mana >= 30)
            {
                HP += 10;
                Atak += 10;
                Mana -= 30;
                Console.WriteLine("Rzuciłeś czar (+10 HP, +10 ataku, -30 many)");
            }
            else
            {
                Console.WriteLine("Brak many");
            }
        }
    }

    class Bandyta : Postac
    {
        public Bandyta() : base("Bandyta", 120, 40, 30, 35) { }
    }

    class Potwor
    {
        public int HP { get; private set; }
        public int Atak { get; private set; }
        public int Mana { get; private set; }
        public int Zloto { get; private set; }

        public Potwor()
        {
            Random rnd = new Random();
            HP = rnd.Next(20, 50);
            Atak = rnd.Next(15, 50);
            Mana = rnd.Next(5, 20);
            Zloto = rnd.Next(3, 40);
        }
    }

    class Gra
    {
        static void Main()
        {
            Console.WriteLine("Wybór postaci: \n1 - Wojownik\n2 - Mag\n3 - Bandyta");
            int input = int.Parse(Console.ReadLine());

            Postac gracz;
            switch (input)
            {
                case 1:
                    gracz = new Wojownik();
                    break;
                case 2:
                    gracz = new Mag();
                    break;
                case 3:
                    gracz = new Bandyta();
                    break;
                default:
                    Console.WriteLine("Zły numer");
                    return;
            }

            while (gracz.HP > 0)
            {
                Console.Clear();
                gracz.Informacje();
                Console.WriteLine("\n=========================\n");
                Console.WriteLine("Co chcesz zrobić?\n1 - Idź do sklepu\n2 - Idź na wyprawę");
                int inp = int.Parse(Console.ReadLine());

                switch (inp)
                {
                    case 1:
                        Sklep(gracz);
                        break;
                    case 2:
                        Wyprawa(gracz);
                        break;
                    default:
                        Console.WriteLine("Zły numer");
                        break;
                }
            }
        }

        static void Sklep(Postac gracz)
        {
            Console.Clear();
            Console.WriteLine("\n*Widzisz ceny na tablic*\n1 - Kup miksturę (+25 HP za 5 złota)\n2 - Kup broń (+20 ataku za 15 złota)\n3 - Kup many (+50 many za 8 złota)\n4 - Wyjdź");
            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    gracz.KupMiksture();
                    break;
                case 2:
                    gracz.KupBron();
                    break;
                case 3:
                    gracz.KupMany();
                    break;
                case 4:
                    Console.WriteLine("\nWychodzisz ze sklepu");
                    break;
                default:
                    Console.WriteLine("\nZły numer");
                    break;
            }
        }

        static void Wyprawa(Postac gracz)
        {
            Potwor potwor = new Potwor();

            while (potwor.HP > 0 && gracz.HP > 0)
            {
                Console.Clear();
                Console.WriteLine("\nSpotykasz potwora\nCo robisz?\n1 - Walczysz\n2 - Używasz Zaklęć\n3 - Spróbuj ucieczki");
                gracz.Informacje();

                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Walka(gracz, potwor);
                        break;
                    case 2:
                        if (gracz is Mag)
                        {
                            (gracz as Mag).RzucCzar();
                        }
                        else
                        {
                            Console.WriteLine("Tylko Mag może używać zaklęć!");
                        }
                        break;
                    case 3:
                        Ucieczka(gracz, potwor);
                        break;
                    default:
                        Console.WriteLine("Zły numer");
                        break;
                }
            }
        }

        static void Walka(Postac gracz, Potwor potwor)
        {
            Console.Clear();
            Console.WriteLine("Walka");

            Console.WriteLine($"\nPotwór zadaje ci {potwor.Atak} obrażeń"); 
            gracz.HP -= potwor.Atak;

            Console.WriteLine($"\nTy zadajesz mu {gracz.Atak} obrażeń"); 
            potwor.HP -= gracz.Atak;

            if (gracz.HP <= 0)
            {
                Console.WriteLine("UMARŁEŚ!!!!");
                return;
            }
            if (potwor.HP <= 0)
            {
                Console.WriteLine($"\nZabierasz potworowi {potwor.Zloto} złota i pobierasz kawałki many z jego duszy");
                gracz.Zloto += potwor.Zloto;
                gracz.Mana += potwor.Mana;
            }
        }

        static void Ucieczka(Postac gracz, Potwor potwor)
        {
            Console.WriteLine("\nUciekasz");
            Random rnd = new Random();
            int ucieczka = rnd.Next(1, 6);

            if (ucieczka == 2 || ucieczka == 5)
            {
                Console.WriteLine("\nUdało ci się uciec");
                potwor.HP = 0;
            }
            else
            {
                gracz.HP -= (potwor.Atak * 2);
                Console.WriteLine("\nNie udało ci się uciec, potwór zadaje ci podwójne obrażenia");
            }
        }
    }
}
