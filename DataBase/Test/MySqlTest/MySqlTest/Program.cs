using System;
using MySql.Data.MySqlClient;

    class Program
    {
    static MySqlConnection conect = null;
        static void Main(string[] args)
        {
            conect=new MySqlConnection("server=localhost;User Id=root;password=123456;DataBase=test;Charset=utf8mb4");
            conect.Open();
        //增
     //   Add();
        //删
        Delete();
        //改
     //   Update();
        //查
        Select();

        Console.ReadKey();
        conect.Close();

        }

        static void Add() {
        MySqlCommand cmd = new MySqlCommand("insert into userinfo(username) values('Hi好3')", conect);
        cmd.ExecuteNonQuery();
        int id = (int)cmd.LastInsertedId;
        Console.WriteLine("Sql Insert Key{0}", id);
        }

    static void Delete()
    {
        MySqlCommand cmd = new MySqlCommand("delete from userinfo   where id=@id", conect);
        cmd.Parameters.AddWithValue("id", 3);
        cmd.ExecuteNonQuery();
    }

    static void Update()
    {
        MySqlCommand cmd = new MySqlCommand("update userinfo   set username=@name where id=@id", conect);

        cmd.Parameters.AddWithValue("name", "小心");
        cmd.Parameters.AddWithValue("id",1);

        cmd.ExecuteNonQuery();
    }

    static void Select()
    {
        MySqlCommand cmd = new MySqlCommand("select * from userinfo", conect);

        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32("id");
            string name = reader.GetString("username");

            Console.WriteLine(string.Format("sql result id:{0} name:{1}", id, name));

        }
    }
}

