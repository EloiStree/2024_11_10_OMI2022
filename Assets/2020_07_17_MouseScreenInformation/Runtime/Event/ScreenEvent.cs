using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScreenEvent : UnityEvent { }
[System.Serializable]
public class NamedScreenPourcentZoneEvent : UnityEvent<NamedScreenPourcentZone> { }
[System.Serializable]
public class NamedScreenPourcentPositionEvent : UnityEvent<NamedScreenPourcentPosition> { }
[System.Serializable]
public class ScreenPourcentPositionBeanEvent : UnityEvent<ScreenPositionInPourcentBean> { }

[System.Serializable]
public class ScreenPourcentZoneBeanEvent : UnityEvent<ScreenZoneInPourcentBean> { }
