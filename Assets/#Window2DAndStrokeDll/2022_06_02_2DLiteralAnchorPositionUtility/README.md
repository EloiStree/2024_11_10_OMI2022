# 2022_06_02_2DLiteralAnchorPositionUtility
I am so tired to use Pixel value in int and percent in float and not knowing in what direction they are...


This repository is an attempt to centreliazed my way to anchor information in Unity in an abstract way.
Because I just anchor position every where:
- Texture2D
- UI in Unity
- User32 Window
- ...

I need a tool that I can relize on with literal name for coordinate.
- Left2Right, Top2Bottom ...
- FromCornerLeftRight ...
- RectAxisRigthDown, RectAxisTopRight...

Something like that.

More the tool will evolve, more I should add utility to convert from one measure to an other.

But I am F**king tired to rewrite those each time I need to anchor an object in a 2D zone just be cause th coordinate and axe are not the same.
The only commun point: they are pixel and percent and they all have rectangle in rectangle in rectangle.
