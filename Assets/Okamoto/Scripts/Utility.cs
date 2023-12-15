using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

//----------------------------------------------------------------
//  汎用関数一覧
//----------------------------------------------------------------
public static class Utility
{
    //----------------------------------------------------------------
    //  列挙子の数を取得します
    //----------------------------------------------------------------
    public static int GetEnumeratorLength<T>() => Enum.GetValues(typeof(T)).Length;

    //----------------------------------------------------------------
    //  データをシリアライズする
    //  [T] data : シリアライズする型
    //  [string] output : 出力先パス
    //----------------------------------------------------------------
    public static void Serialize<T>(T data, string output)
    {
        //ファイル生成
        using var fs = new FileStream(output, FileMode.Create);

        //シリアライザ生成
        var serializer = new DataContractSerializer(typeof(T));

        //シリアライズ
        serializer.WriteObject(fs, data);
    }

    //----------------------------------------------------------------
    //  データをデシリアライズする
    //  [string] output : 出力先パス
    //----------------------------------------------------------------
    public static T Deserialize<T>(string from)
    {
        //ファイル情報取得
        using var fs = new FileStream(from, FileMode.Open);

        //シリアライザ生成
        var serializer = new DataContractSerializer(typeof(T));

        //シリアライズ
        var instance = (T)serializer.ReadObject(fs);

        return instance;
    }
}