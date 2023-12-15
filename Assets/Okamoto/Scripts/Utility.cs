using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

//----------------------------------------------------------------
//  �ėp�֐��ꗗ
//----------------------------------------------------------------
public static class Utility
{
    //----------------------------------------------------------------
    //  �񋓎q�̐����擾���܂�
    //----------------------------------------------------------------
    public static int GetEnumeratorLength<T>() => Enum.GetValues(typeof(T)).Length;

    //----------------------------------------------------------------
    //  �f�[�^���V���A���C�Y����
    //  [T] data : �V���A���C�Y����^
    //  [string] output : �o�͐�p�X
    //----------------------------------------------------------------
    public static void Serialize<T>(T data, string output)
    {
        //�t�@�C������
        using var fs = new FileStream(output, FileMode.Create);

        //�V���A���C�U����
        var serializer = new DataContractSerializer(typeof(T));

        //�V���A���C�Y
        serializer.WriteObject(fs, data);
    }

    //----------------------------------------------------------------
    //  �f�[�^���f�V���A���C�Y����
    //  [string] output : �o�͐�p�X
    //----------------------------------------------------------------
    public static T Deserialize<T>(string from)
    {
        //�t�@�C�����擾
        using var fs = new FileStream(from, FileMode.Open);

        //�V���A���C�U����
        var serializer = new DataContractSerializer(typeof(T));

        //�V���A���C�Y
        var instance = (T)serializer.ReadObject(fs);

        return instance;
    }
}