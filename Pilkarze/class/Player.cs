using System;
using System.IO;
using System.Xml.Serialization;

public class Player
{
	//public int waga;
	//private int wiek;
	//public string imie;
	//private string nazwisko;


	[XmlElement("Age")]
	public int Age { get; set; }
	[XmlElement("Weight")]
	public double Weight { get; set; }
	[XmlElement("Name")]
	public string Name { get; set; }
	[XmlElement("Surname")]
	public string Surname { get; set; }

	public Player()
    {
    } 
	
	public Player( int age, double weight, string name, string surname)
    {
		Name = name;
		Surname = surname;
		Age = age;
		Weight = weight;
	}

	public Player(Player o)
    {
		Name = o.Name;
		Surname = o.Surname;
		Age = o.Age;
		Weight = o.Weight;
    }

    public override string ToString()
    {
        return $"{Surname} {Name}, waga: {Weight}[kg], wiek: {Age}[lat]";
    }




}
