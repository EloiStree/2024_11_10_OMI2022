using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanSpliterMono : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public BooleanSpliterRegister m_spliters;
    public BooleanConditionSpliterRegister m_splitersCondition= new BooleanConditionSpliterRegister();


    public void ReloadAllSpliters() {

        m_splitersCondition.Clear();
        for (int i = 0; i < m_spliters.m_duo.Count; i++)
        {
            DuoSpliterCondition value = new DuoSpliterCondition();
            value.m_spliter = m_spliters.m_duo[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_duo[i].m_firstCondition, out value.m_first);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_duo[i].m_secondCondition, out value.m_second);
            m_splitersCondition.m_duo.Add(value);
        }
        for (int i = 0; i < m_spliters.m_trio.Count; i++)
        {
            TrioLineSpliterCondition value = new TrioLineSpliterCondition();
            value.m_spliter = m_spliters.m_trio[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_trio[i].m_firstCondition, out value.m_first);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_trio[i].m_secondCondition, out value.m_second);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_trio[i].m_thirdCondition, out value.m_third);
            m_splitersCondition.m_trio.Add(value);
        }
        for (int i = 0; i < m_spliters.m_quatro.Count; i++)
        {
            QuatroLineSpliterCondition value = new QuatroLineSpliterCondition();
            value.m_spliter = m_spliters.m_quatro[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_quatro[i].m_firstCondition, out value.m_first);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_quatro[i].m_secondCondition, out value.m_second);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_quatro[i].m_thirdCondition, out value.m_third);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_quatro[i].m_fourthCondition, out value.m_fourst);
            m_splitersCondition.m_quatro.Add(value);
        }
        for (int i = 0; i < m_spliters.m_fingers.Count; i++)
        {
            FingerSpliterCondition value = new FingerSpliterCondition();
            value.m_spliter = m_spliters.m_fingers[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_fingers[i].m_pinkyCondition, out value.m_first);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_fingers[i].m_ringCondition, out value.m_second);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_fingers[i].m_middleCondition, out value.m_third);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_fingers[i].m_indexCondition, out value.m_fourst);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_fingers[i].m_thumbCondition, out value.m_fifth);
            m_splitersCondition.m_fingers.Add(value);
        }
        for (int i = 0; i < m_spliters.m_diamond.Count; i++)
        {
            DiamondSpliterCondition value = new DiamondSpliterCondition();
            value.m_spliter = m_spliters.m_diamond[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_diamond[i].m_leftCondition, out value.m_left);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_diamond[i].m_rightCondition, out value.m_right);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_diamond[i].m_forwardCondition, out value.m_up);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_diamond[i].m_backwardCondition, out value.m_down);
            m_splitersCondition.m_diamond.Add(value);
        }
        for (int i = 0; i < m_spliters.m_square.Count; i++)
        {
            SquareSpliterCondition value = new SquareSpliterCondition();
            value.m_spliter = m_spliters.m_square[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_square[i].m_leftTopCondition, out value.m_leftTop);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_square[i].m_rightTopCondition, out value.m_rightTop);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_square[i].m_leftDownCondition, out value.m_leftDown);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_square[i].m_rightDownCondition, out value.m_rightDown);
            m_splitersCondition.m_square.Add(value);
        }

        for (int i = 0; i < m_spliters.m_joystick2D.Count; i++)
        {
            Joystick2DSpliterCondition value = new Joystick2DSpliterCondition();
            value.m_spliter = m_spliters.m_joystick2D[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick2D[i].m_leftCondition, out value.m_left);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick2D[i].m_rightCondition, out value.m_right);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick2D[i].m_forwardCondition, out value.m_forward);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick2D[i].m_backwardCondition, out value.m_backward);
            m_splitersCondition.m_joystick2D.Add(value);
        }
        for (int i = 0; i < m_spliters.m_joystick3D.Count; i++)
        {
            Joystick3DSpliterCondition value = new Joystick3DSpliterCondition();
            value.m_spliter = m_spliters.m_joystick3D[i];
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick3D[i].m_leftCondition, out value.m_left);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick3D[i].m_rightCondition, out value.m_right);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick3D[i].m_forwardCondition, out value.m_forward);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick3D[i].m_backwardCondition, out value.m_backward);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick3D[i].m_upCondition, out value.m_up);
            TextToBoolStateMachineParser.IsClassicParse(m_spliters.m_joystick3D[i].m_downRenamed, out value.m_down);
            m_splitersCondition.m_joystick3D.Add(value);
        }
    }

    public void CheckAllConditions() {

        BooleanStateRegister register = m_register.m_register;
        for (int i = 0; i < m_splitersCondition.m_duo.Count; i++)
        {
            DuoSpliterCondition value = m_splitersCondition.m_duo[i];
            bool first, second;
            if (value.m_first.IsBooleansRegistered(register) && value.m_second.IsBooleansRegistered(register)) {
                first = value.m_first.IsConditionValide(register);
                second = value.m_second.IsConditionValide(register);

                if (!string.IsNullOrEmpty(value.m_spliter.m_none ))
                {
                    register.Set(value.m_spliter.m_none, !first && !second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_all ))
                {
                    register.Set(value.m_spliter.m_all, first && second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_firstRenamed ))
                {
                    register.Set(value.m_spliter.m_firstRenamed, first);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_secondRenamed ))
                {
                    register.Set(value.m_spliter.m_secondRenamed, second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_onlyfirst ))
                {
                    register.Set(value.m_spliter.m_onlyfirst, first && !second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_onlysecond ))
                {
                    register.Set(value.m_spliter.m_onlysecond, !first && second );
                }

            }
        }
        for (int i = 0; i < m_splitersCondition.m_trio.Count; i++)
        {
            TrioLineSpliterCondition value = m_splitersCondition.m_trio[i];
            bool first, second, third;
            if (value.m_first.IsBooleansRegistered(register) && value.m_second.IsBooleansRegistered(register) && value.m_third.IsBooleansRegistered(register))
            {
                first = value.m_first.IsConditionValide(register);
                second = value.m_second.IsConditionValide(register);
                third = value.m_third.IsConditionValide(register);
                CheckConditionToRegister(ref register, ref value.m_spliter, first, second, third); ;

                if (!string.IsNullOrEmpty(value.m_spliter.m_firstRenamed))
                {
                    register.Set(value.m_spliter.m_firstRenamed, first);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_secondRenamed))
                {
                    register.Set(value.m_spliter.m_secondRenamed, second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_thirdRenamed))
                {
                    register.Set(value.m_spliter.m_thirdRenamed, third);
                }


            }

        }
        for (int i = 0; i < m_splitersCondition.m_quatro.Count; i++)
        {
            QuatroLineSpliterCondition value = m_splitersCondition.m_quatro[i];
            bool first, second, third, fourst;
            if (value.m_first.IsBooleansRegistered(register) && value.m_second.IsBooleansRegistered(register)
                && value.m_third.IsBooleansRegistered(register) && value.m_fourst.IsBooleansRegistered(register))
            {
                first = value.m_first.IsConditionValide(register);
                second = value.m_second.IsConditionValide(register);
                third = value.m_third.IsConditionValide(register);
                fourst = value.m_fourst.IsConditionValide(register);
                CheckConditionToRegister(ref register, ref value.m_spliter, first, second, third, fourst);

                if (!string.IsNullOrEmpty(value.m_spliter.m_firstRenamed))
                {
                    register.Set(value.m_spliter.m_firstRenamed, first);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_secondRenamed))
                {
                    register.Set(value.m_spliter.m_secondRenamed, second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_thirdRenamed))
                {
                    register.Set(value.m_spliter.m_thirdRenamed, third);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_fourthRenamed))
                {
                    register.Set(value.m_spliter.m_thirdRenamed, fourst);
                }


            }

        }
        for (int i = 0; i < m_splitersCondition.m_fingers.Count; i++)
        {
            FingerSpliterCondition value = m_splitersCondition.m_fingers[i];
            bool first, second, third, fourst, fifth;
            if (value.m_first.IsBooleansRegistered(register) && value.m_second.IsBooleansRegistered(register)
                && value.m_third.IsBooleansRegistered(register) && value.m_fourst.IsBooleansRegistered(register) && value.m_fifth.IsBooleansRegistered(register))
            {
                first = value.m_first.IsConditionValide(register);
                second = value.m_second.IsConditionValide(register);
                third = value.m_third.IsConditionValide(register);
                fourst = value.m_fourst.IsConditionValide(register);
                fifth = value.m_fifth.IsConditionValide(register);
                CheckConditionToRegister(ref register, ref value.m_spliter, first, second, third, fourst, fifth);

                if (!string.IsNullOrEmpty(value.m_spliter.m_pinkyRenamed))
                {
                    register.Set(value.m_spliter.m_pinkyRenamed, first);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_ringRenamed))
                {
                    register.Set(value.m_spliter.m_ringRenamed, second);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_middleRenamed))
                {
                    register.Set(value.m_spliter.m_middleRenamed, third);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_indexRenamed))
                {
                    register.Set(value.m_spliter.m_indexRenamed, fourst);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_thumbRenamed))
                {
                    register.Set(value.m_spliter.m_thumbRenamed, fifth);
                }


            }

        }


        for (int i = 0; i < m_splitersCondition.m_square.Count; i++)
        {
            SquareSpliterCondition value = m_splitersCondition.m_square[i];
            bool lt, rt, ld, rd;
            if (value.m_leftDown.IsBooleansRegistered(register) && value.m_leftTop.IsBooleansRegistered(register)
                && value.m_rightDown.IsBooleansRegistered(register) && value.m_rightTop.IsBooleansRegistered(register))
            {
                lt = value.m_leftTop.IsConditionValide(register);
                ld = value.m_leftDown.IsConditionValide(register);
                rt = value.m_rightTop.IsConditionValide(register);
                rd = value.m_rightDown.IsConditionValide(register);

                if (!string.IsNullOrEmpty(value.m_spliter.m_all)) register.Set(value.m_spliter.m_all, ld && lt && rd && rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_none)) register.Set(value.m_spliter.m_none, !ld && !lt && !rd && !rt);

                if (!string.IsNullOrEmpty(value.m_spliter.m_onlydownleft)) register.Set(value.m_spliter.m_onlydownleft, ld && !lt && !rd && !rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_onlydownright)) register.Set(value.m_spliter.m_onlydownright, !ld && !lt && rd && !rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_onlytopleft)) register.Set(value.m_spliter.m_onlytopleft, !ld && lt && !rd && !rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_onlytopright)) register.Set(value.m_spliter.m_onlytopright, !ld && !lt && !rd && rt);


                if (!string.IsNullOrEmpty(value.m_spliter.m_fulldown)) register.Set(value.m_spliter.m_fulldown, ld && !lt && rd && !rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_fullup)) register.Set(value.m_spliter.m_fullup, !ld && lt && !rd && rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_fullleft)) register.Set(value.m_spliter.m_fullleft, ld && lt && !rd && !rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_fullright)) register.Set(value.m_spliter.m_fullright, !ld && !lt && rd && rt);


                if (!string.IsNullOrEmpty(value.m_spliter.m_allexceptdownleft)) register.Set(value.m_spliter.m_allexceptdownleft, !ld && lt && rd && rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_allexceptdownright)) register.Set(value.m_spliter.m_allexceptdownright, ld && lt && !rd && rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_allexcepttopleft)) register.Set(value.m_spliter.m_allexcepttopleft, ld && !lt && rd && rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_allexcepttopright)) register.Set(value.m_spliter.m_allexcepttopright, ld && lt && rd && !rt);

                if (!string.IsNullOrEmpty(value.m_spliter.m_slashdiagonal)) register.Set(value.m_spliter.m_slashdiagonal, ld && !lt && !rd && rt);
                if (!string.IsNullOrEmpty(value.m_spliter.m_backslashdiagonal)) register.Set(value.m_spliter.m_backslashdiagonal, !ld && lt && rd && !rt);


                if (!string.IsNullOrEmpty(value.m_spliter.m_leftTopRenamed))
                {
                    register.Set(value.m_spliter.m_leftTopRenamed, lt);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_leftDownRenamed))
                {
                    register.Set(value.m_spliter.m_leftDownRenamed, ld);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_rightTopRenamed))
                {
                    register.Set(value.m_spliter.m_rightTopRenamed, rt);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_rightDownRenamed))
                {
                    register.Set(value.m_spliter.m_rightDownRenamed, rd);
                }
                


            }

        }

        for (int i = 0; i < m_splitersCondition.m_diamond.Count; i++)
        {
            DiamondSpliterCondition value = m_splitersCondition.m_diamond[i];
            bool left, up, right, down;
            if (value.m_left.IsBooleansRegistered(register) && value.m_right.IsBooleansRegistered(register)
                && value.m_up.IsBooleansRegistered(register) && value.m_down.IsBooleansRegistered(register))
            {
                left = value.m_left.IsConditionValide(register);
                right = value.m_right.IsConditionValide(register);
                up = value.m_up.IsConditionValide(register);
                down = value.m_down.IsConditionValide(register);

                if (!string.IsNullOrEmpty(value.m_spliter.m_all)) register.Set(value.m_spliter.m_all, right && left && down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_none)) register.Set(value.m_spliter.m_none, !right && !left && !down && !up);

                if (!string.IsNullOrEmpty(value.m_spliter.m_fulldown)) register.Set(value.m_spliter.m_fulldown, right && left && down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_fullup)) register.Set(value.m_spliter.m_fullup, right && left && !down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_fullleft)) register.Set(value.m_spliter.m_fullleft, !right && left && down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_fullright)) register.Set(value.m_spliter.m_fullright, right && !left && down && up);


                if (!string.IsNullOrEmpty(value.m_spliter.m_horizontal)) register.Set(value.m_spliter.m_horizontal, right && left && !down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_vertical)) register.Set(value.m_spliter.m_vertical, !right && !left && down && up);

                if (!string.IsNullOrEmpty(value.m_spliter.m_se)) register.Set(value.m_spliter.m_se, right && !left && !down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_sw)) register.Set(value.m_spliter.m_sw, !right && left && !down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_ne)) register.Set(value.m_spliter.m_ne, right && !left && down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_nw)) register.Set(value.m_spliter.m_nw, !right && left && down && !up);


                if (!string.IsNullOrEmpty(value.m_spliter.m_leftRenamed))
                {
                    register.Set(value.m_spliter.m_leftRenamed, left);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_rightRenamed))
                {
                    register.Set(value.m_spliter.m_rightRenamed, right);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_forwardRenamed))
                {
                    register.Set(value.m_spliter.m_forwardRenamed, up);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_backwardRenamed))
                {
                    register.Set(value.m_spliter.m_backwardRenamed, down);
                }



            }

        }



        for (int i = 0; i < m_splitersCondition.m_joystick2D.Count; i++)
        {
            Joystick2DSpliterCondition value = m_splitersCondition.m_joystick2D[i];
            bool left, up, right, down;
            if (value.m_left.IsBooleansRegistered(register) && value.m_right.IsBooleansRegistered(register)
                && value.m_forward.IsBooleansRegistered(register) && value.m_backward.IsBooleansRegistered(register))
            {
                left = value.m_left.IsConditionValide(register);
                right = value.m_right.IsConditionValide(register);
                up = value.m_forward.IsConditionValide(register);
                down = value.m_backward.IsConditionValide(register);

                if (!string.IsNullOrEmpty(value.m_spliter.m_none)) register.Set(value.m_spliter.m_none, !right && !left && !down && !up);

                if (!string.IsNullOrEmpty(value.m_spliter.m_s)) register.Set(value.m_spliter.m_s, !right && !left && down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_n)) register.Set(value.m_spliter.m_n, !right && !left && !down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_w)) register.Set(value.m_spliter.m_w, right && !left && !down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_e)) register.Set(value.m_spliter.m_e, !right && left && !down && !up);

                if (!string.IsNullOrEmpty(value.m_spliter.m_se)) register.Set(value.m_spliter.m_se,right && !left && down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_sw)) register.Set(value.m_spliter.m_sw,!right && left && down && !up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_ne)) register.Set(value.m_spliter.m_ne,right && !left && !down && up);
                if (!string.IsNullOrEmpty(value.m_spliter.m_nw)) register.Set(value.m_spliter.m_nw,!right && left && !down && up);


                if (!string.IsNullOrEmpty(value.m_spliter.m_leftRenamed))
                {
                    register.Set(value.m_spliter.m_leftRenamed, left);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_rightRenamed))
                {
                    register.Set(value.m_spliter.m_rightRenamed, right);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_forwardRenamed))
                {
                    register.Set(value.m_spliter.m_forwardRenamed, up);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_backwardRenamed))
                {
                    register.Set(value.m_spliter.m_backwardRenamed, down);
                }

            }

        }
        
        for (int i = 0; i < m_splitersCondition.m_joystick3D.Count; i++)
        {
            Joystick3DSpliterCondition value = m_splitersCondition.m_joystick3D[i];
            bool left, up, right, down, forward, backward;
            if (value.m_left.IsBooleansRegistered(register) && value.m_right.IsBooleansRegistered(register)
                && value.m_forward.IsBooleansRegistered(register) && value.m_backward.IsBooleansRegistered(register)
                && value.m_up.IsBooleansRegistered(register) && value.m_down.IsBooleansRegistered(register))
            {
                left = value.m_left.IsConditionValide(register);
                right = value.m_right.IsConditionValide(register);
                forward = value.m_forward.IsConditionValide(register);
                backward = value.m_backward.IsConditionValide(register);
                up = value.m_up.IsConditionValide(register);
                down = value.m_down.IsConditionValide(register);

                CheckConditionToRegister(ref register, ref value.m_spliter, up, down, left, right, forward, backward );

                if (!string.IsNullOrEmpty(value.m_spliter.m_leftRenamed))
                {
                    register.Set(value.m_spliter.m_leftRenamed, left);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_rightRenamed))
                {
                    register.Set(value.m_spliter.m_rightRenamed, right);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_forwardRenamed))
                {
                    register.Set(value.m_spliter.m_forwardRenamed, forward);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_backwardRenamed))
                {
                    register.Set(value.m_spliter.m_backwardRenamed, backward);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_upRenamed))
                {
                    register.Set(value.m_spliter.m_upCondition, up);
                }
                if (!string.IsNullOrEmpty(value.m_spliter.m_downRenamed))
                {
                    register.Set(value.m_spliter.m_downRenamed, down);
                }

         }

        }
   
    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref TrioLineSpliter value, bool first, bool second, bool third)
    {
        foreach (TrioBinary selection in (TrioBinary[])Enum.GetValues(typeof(TrioBinary)))
        {
            CheckConditionToRegister(ref register, ref value, selection, first, second, third);
        }

    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref QuatroLineSpliter value, bool first, bool second, bool third, bool fourst)
    {
        foreach (QuatroBinary selection in (QuatroBinary[])Enum.GetValues(typeof(QuatroBinary)))
        {
            CheckConditionToRegister(ref register, ref value, selection, first, second, third, fourst);
        }

    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref FingerSpliter value, bool first, bool second, bool third, bool fourst, bool fifth)
    {
        foreach (FingerBinary selection in (FingerBinary[])Enum.GetValues(typeof(FingerBinary)))
        {
            CheckConditionToRegister(ref register, ref value, selection, first, second, third, fourst, fifth);
        }

    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref Joystick3DSpliter value, bool up, bool down, bool left, bool right, bool forward, bool backward)
    {
        foreach (Joystick3DEnum selection in (Joystick3DEnum[])Enum.GetValues(typeof(Joystick3DEnum)))
        {
            CheckConditionToRegister(ref register, ref value, selection,  up,  down,  left,  right,  forward,  backward);
        }

    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref TrioLineSpliter value, TrioBinary selection, bool first, bool second, bool third)
    {
        string name = value.Get(selection);
        bool logicValue = GetValueOf(selection, first, second, third);
        if (!string.IsNullOrEmpty(name))
        {
            register.Set(name, logicValue);
        }
    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref QuatroLineSpliter value, QuatroBinary selection, bool first, bool second, bool third, bool fourst)
    {
        string name = value.Get(selection);
        bool logicValue = GetValueOf(selection, first, second, third, fourst);
        if (!string.IsNullOrEmpty(name))
        {
            register.Set(name, logicValue);
        }
    }
    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref FingerSpliter value, FingerBinary selection, bool first, bool second, bool third, bool fourst, bool fifth)
    {
        string name = value.Get(selection);
        bool logicValue = GetValueOf(selection, first, second, third, fourst, fifth);
        if (!string.IsNullOrEmpty(name))
        {
            register.Set(name, logicValue);
        }
    }

    private static void CheckConditionToRegister(ref BooleanStateRegister register, ref Joystick3DSpliter value, Joystick3DEnum selection, bool up, bool down, bool left, bool right, bool forward, bool backward)
    {
        string name = value.Get(selection);
        bool logicValue = GetValueOf(selection, up, down, left, right, forward, backward);
        if (!string.IsNullOrEmpty(name))
        {
            register.Set(name, logicValue);
        }
    }

    private static bool GetValueOf(TrioBinary selection, bool first, bool second, bool third)
    {
        switch (selection)
        {
            case TrioBinary.ooo: return !first && !second && !third;
            case TrioBinary.ooi: return !first && !second && third;
            case TrioBinary.oio: return !first && second && !third;
            case TrioBinary.oii: return !first && second && third;
            case TrioBinary.ioo: return first && !second && !third;
            case TrioBinary.ioi: return first && !second && third;
            case TrioBinary.iio: return first && second && !third;
            case TrioBinary.iii: return first && second && third;
            default:
                break;
        }
        return false;
    }

    private static bool GetValueOf(Joystick3DEnum selection, bool up, bool down, bool left, bool right, bool forward, bool backward )
    {
        switch (selection)
        {
            case Joystick3DEnum.TN: return (up && !down) &&   (!left && !right && forward && !backward);
            case Joystick3DEnum.TS: return (up && !down) &&   (!left && !right && !forward && backward);
            case Joystick3DEnum.TW: return (up && !down) &&   (left && !right && !forward && !backward);
            case Joystick3DEnum.TE: return (up && !down) &&   (!left && right && !forward && !backward);
            case Joystick3DEnum.TNE: return (up && !down) &&  (!left && right && forward && !backward);
            case Joystick3DEnum.TNW: return (up && !down) &&  (left && !right && forward && !backward);
            case Joystick3DEnum.TSE: return (up && !down) &&  (!left && right && !forward && backward);
            case Joystick3DEnum.TSW: return (up && !down) &&  (left && !right && !forward && backward);
            case Joystick3DEnum.TC: return (up && !down) &&  (!left && !right && !forward && !backward);

            case Joystick3DEnum.DN: return (!up && down) &&   (!left && !right && forward && !backward);
            case Joystick3DEnum.DS: return (!up && down) &&   (!left && !right && !forward && backward);
            case Joystick3DEnum.DW: return (!up && down) &&   (left && !right && !forward && !backward);
            case Joystick3DEnum.DE: return (!up && down) &&   (!left && right && !forward && !backward);
            case Joystick3DEnum.DNE: return (!up && down) &&  (!left && right && forward && !backward);
            case Joystick3DEnum.DNW: return (!up && down) &&  (left && !right && forward && !backward);
            case Joystick3DEnum.DSE: return (!up && down) &&  (!left && right && !forward && backward);
            case Joystick3DEnum.DSW: return (!up && down) &&  (left && !right && !forward && backward);
            case Joystick3DEnum.DC: return (!up && down) &&   (!left && !right && !forward && !backward);

            case Joystick3DEnum.MN: return (!up && !down) &&  (!left && !right && forward && !backward);
            case Joystick3DEnum.MS: return (!up && !down) &&  (!left && !right && !forward && backward);
            case Joystick3DEnum.MW: return (!up && !down) &&  (left && !right && !forward && !backward);
            case Joystick3DEnum.ME: return (!up && !down) &&  (!left && right && !forward && !backward);
            case Joystick3DEnum.MNE: return (!up && !down) && (!left && right && forward && !backward);
            case Joystick3DEnum.MNW: return (!up && !down) && (left && !right && forward && !backward);
            case Joystick3DEnum.MSE: return (!up && !down) && (!left && right && !forward && backward);
            case Joystick3DEnum.MSW: return (!up && !down) && (left && !right && !forward && backward);
            case Joystick3DEnum.MC: return (!up && !down) && (!left && !right && !forward && !backward);
            case Joystick3DEnum.Neutral: return (!up && !down) && (!left && !right && !forward && !backward);
            default:
                break;
        }
        return false;
    }
    

    private static bool GetValueOf(QuatroBinary selection, bool first, bool second, bool third, bool fourst)
    {
        switch (selection)
        {
            case QuatroBinary.oooo: return !first && !second && !third && !fourst;
            case QuatroBinary.oooi: return !first && !second && !third && fourst;
            case QuatroBinary.ooio: return !first && !second && third && !fourst;
            case QuatroBinary.ooii: return !first && !second && third && fourst;
            case QuatroBinary.oioo: return !first && second && !third && !fourst;
            case QuatroBinary.oioi: return !first && second && !third && fourst;
            case QuatroBinary.oiio: return !first && second && third && !fourst;
            case QuatroBinary.oiii: return !first && second && third && fourst;
            case QuatroBinary.iooo: return first && !second && !third && !fourst;
            case QuatroBinary.iooi: return first && !second && !third && fourst;
            case QuatroBinary.ioio: return first && !second && third && !fourst;
            case QuatroBinary.ioii: return first && !second && third && fourst;
            case QuatroBinary.iioo: return first && second && !third && !fourst;
            case QuatroBinary.iioi: return first && second && !third && fourst;
            case QuatroBinary.iiio: return first && second && third && !fourst;
            case QuatroBinary.iiii: return first && second && third && fourst;
            default:
                break;
        }
        return false;
    }
    private static bool GetValueOf(FingerBinary selection, bool first, bool second, bool third, bool fourst, bool fifth)
    {
        switch (selection)
        {
            case FingerBinary.ooooo: return !first && !second && !third && !fourst && !fifth;
            case FingerBinary.ooooi: return !first && !second && !third && !fourst && fifth;
            case FingerBinary.oooio: return !first && !second && !third && fourst && !fifth;
            case FingerBinary.oooii: return !first && !second && !third && fourst && fifth;
            case FingerBinary.ooioo: return !first && !second && third && !fourst && !fifth;
            case FingerBinary.ooioi: return !first && !second && third && !fourst && fifth;
            case FingerBinary.ooiio: return !first && !second && third && fourst && !fifth;
            case FingerBinary.ooiii: return !first && !second && third && fourst && fifth;
            case FingerBinary.oiooo: return !first && second && !third && !fourst && !fifth;
            case FingerBinary.oiooi: return !first && second && !third && !fourst && fifth;
            case FingerBinary.oioio: return !first && second && !third && fourst && !fifth;
            case FingerBinary.oioii: return !first && second && !third && fourst && fifth;
            case FingerBinary.oiioo: return !first && second && third && !fourst && !fifth;
            case FingerBinary.oiioi: return !first && second && third && !fourst && fifth;
            case FingerBinary.oiiio: return !first && second && third && fourst && !fifth;
            case FingerBinary.oiiii: return !first && second && third && fourst && fifth;
            case FingerBinary.ioooo: return first && !second && !third && !fourst && !fifth;
            case FingerBinary.ioooi: return first && !second && !third && !fourst && fifth;
            case FingerBinary.iooio: return first && !second && !third && fourst && !fifth;
            case FingerBinary.iooii: return first && !second && !third && fourst && fifth;
            case FingerBinary.ioioo: return first && !second && third && !fourst && !fifth;
            case FingerBinary.ioioi: return first && !second && third && !fourst && fifth;
            case FingerBinary.ioiio: return first && !second && third && fourst && !fifth;
            case FingerBinary.ioiii: return first && !second && third && fourst && fifth;
            case FingerBinary.iiooo: return first && second && !third && !fourst && !fifth;
            case FingerBinary.iiooi: return first && second && !third && !fourst && fifth;
            case FingerBinary.iioio: return first && second && !third && fourst && !fifth;
            case FingerBinary.iioii: return first && second && !third && fourst && fifth;
            case FingerBinary.iiioo: return first && second && third && !fourst && !fifth;
            case FingerBinary.iiioi: return first && second && third && !fourst && fifth;
            case FingerBinary.iiiio: return first && second && third && fourst && !fifth;
            case FingerBinary.iiiii: return first && second && third && fourst && fifth;
            default:
                break;
        }
        return false;
    }
}

[System.Serializable]
public class BooleanConditionSpliterRegister 
{
    public void Clear()
    {
        m_duo.Clear();
        m_trio.Clear();
        m_quatro.Clear();
        m_fingers.Clear();
        m_diamond.Clear();
        m_square.Clear();
        m_joystick2D.Clear();
        m_joystick3D.Clear();
    }
    public List<DuoSpliterCondition> m_duo = new List<DuoSpliterCondition>();
    public List<TrioLineSpliterCondition> m_trio = new List<TrioLineSpliterCondition>();
    public List<QuatroLineSpliterCondition> m_quatro = new List<QuatroLineSpliterCondition>();
    public List<FingerSpliterCondition> m_fingers = new List<FingerSpliterCondition >();
    public List<DiamondSpliterCondition> m_diamond = new List<DiamondSpliterCondition>();
    public List<SquareSpliterCondition> m_square = new List<SquareSpliterCondition>();

    
    public List<Joystick2DSpliterCondition> m_joystick2D = new List<Joystick2DSpliterCondition>();
    public List<Joystick3DSpliterCondition> m_joystick3D = new List<Joystick3DSpliterCondition>();
    
}
public class Joystick2DSpliterCondition
{
    public Joystick2DSpliter m_spliter;
    public ClassicBoolState m_left;
    public ClassicBoolState m_right;
    public ClassicBoolState m_forward;
    public ClassicBoolState m_backward;
}
public class Joystick3DSpliterCondition
{
    public Joystick3DSpliter m_spliter;
    public ClassicBoolState m_left;
    public ClassicBoolState m_right;
    public ClassicBoolState m_forward;
    public ClassicBoolState m_backward;
    public ClassicBoolState m_up;
    public ClassicBoolState m_down;
}
public class DuoSpliterCondition
{
    public DuoSpliter m_spliter;
    public ClassicBoolState m_first;
    public ClassicBoolState m_second;
}
public class TrioLineSpliterCondition
{
    public TrioLineSpliter m_spliter;
    public ClassicBoolState m_first;
    public ClassicBoolState m_second;
    public ClassicBoolState m_third;

  
}
public class QuatroLineSpliterCondition
{
    public QuatroLineSpliter m_spliter;
    public ClassicBoolState m_first;
    public ClassicBoolState m_second;
    public ClassicBoolState m_third;
    public ClassicBoolState m_fourst;

}
public class FingerSpliterCondition
{
    public FingerSpliter m_spliter;
    public ClassicBoolState m_first;
    public ClassicBoolState m_second;
    public ClassicBoolState m_third;
    public ClassicBoolState m_fourst;
    public ClassicBoolState m_fifth;

}

public class DiamondSpliterCondition
{
    public DiamondSpliter m_spliter;
    public ClassicBoolState m_left;
    public ClassicBoolState m_right;
    public ClassicBoolState m_up;
    public ClassicBoolState m_down;
}
public class SquareSpliterCondition
{
    public SquareSpliter m_spliter;
    public ClassicBoolState m_leftTop;
    public ClassicBoolState m_leftDown;
    public ClassicBoolState m_rightTop;
    public ClassicBoolState m_rightDown;
}