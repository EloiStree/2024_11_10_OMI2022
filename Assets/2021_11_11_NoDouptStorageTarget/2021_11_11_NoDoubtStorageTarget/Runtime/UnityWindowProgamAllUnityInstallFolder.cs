using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityWindowProgamAllUnityInstallFolder : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
     =>usualNameOfIt = "Hub Unity Editor Install Folder";
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    => path = new MetaAbsolutePathDirectory("C:\\Program Files\\Unity\\Hub\\Editor");
    public override void GetShortLineDescriptionOfIt(out string shortDescription)
   => shortDescription = "This folder is where the Unity version are install by default on the computer of the user if he don't change it.";
    
}
