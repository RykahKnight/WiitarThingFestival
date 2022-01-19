using System;
using System.Collections.Generic;
using System.Linq;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using NintrollerLib;

namespace WiitarThing.Holders
{
    public class XInputHolder : Holder
    {
        static internal bool[] availabe = { true, true, true, true };

        internal int minRumble = 20;
        internal int rumbleLeft = 0;
        internal int rumbleDecrement = 10;

        private XBus bus;
        private bool connected;
        private int ID;
        private Dictionary<string, float> writeReport;

        public static Dictionary<string, string> GetDefaultMapping(ControllerType type)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            // TODO: finish default mapping (Acc, IR, Balance Board, ect) (not for 1st release)
            switch (type)
            {
                case ControllerType.ProController:
                    result.Add(Inputs.ProController.A, Inputs.Xbox360.A);
                    result.Add(Inputs.ProController.B, Inputs.Xbox360.B);
                    result.Add(Inputs.ProController.X, Inputs.Xbox360.X);
                    result.Add(Inputs.ProController.Y, Inputs.Xbox360.Y);

                    result.Add(Inputs.ProController.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.ProController.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.ProController.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.ProController.RIGHT, Inputs.Xbox360.RIGHT);

                    result.Add(Inputs.ProController.L, Inputs.Xbox360.LB);
                    result.Add(Inputs.ProController.R, Inputs.Xbox360.RB);
                    result.Add(Inputs.ProController.ZL, Inputs.Xbox360.LT);
                    result.Add(Inputs.ProController.ZR, Inputs.Xbox360.RT);

                    result.Add(Inputs.ProController.LUP, Inputs.Xbox360.LUP);
                    result.Add(Inputs.ProController.LDOWN, Inputs.Xbox360.LDOWN);
                    result.Add(Inputs.ProController.LLEFT, Inputs.Xbox360.LLEFT);
                    result.Add(Inputs.ProController.LRIGHT, Inputs.Xbox360.LRIGHT);

                    result.Add(Inputs.ProController.RUP, Inputs.Xbox360.RUP);
                    result.Add(Inputs.ProController.RDOWN, Inputs.Xbox360.RDOWN);
                    result.Add(Inputs.ProController.RLEFT, Inputs.Xbox360.RLEFT);
                    result.Add(Inputs.ProController.RRIGHT, Inputs.Xbox360.RRIGHT);

                    result.Add(Inputs.ProController.LS, Inputs.Xbox360.LS);
                    result.Add(Inputs.ProController.RS, Inputs.Xbox360.RS);
                    result.Add(Inputs.ProController.SELECT, Inputs.Xbox360.BACK);
                    result.Add(Inputs.ProController.START, Inputs.Xbox360.START);
                    result.Add(Inputs.ProController.HOME, Inputs.Xbox360.GUIDE);
                    break;

                case ControllerType.ClassicControllerPro:
                    result.Add(Inputs.ClassicControllerPro.A, Inputs.Xbox360.A);
                    result.Add(Inputs.ClassicControllerPro.B, Inputs.Xbox360.B);
                    result.Add(Inputs.ClassicControllerPro.X, Inputs.Xbox360.X);
                    result.Add(Inputs.ClassicControllerPro.Y, Inputs.Xbox360.Y);

                    result.Add(Inputs.ClassicControllerPro.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.ClassicControllerPro.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.ClassicControllerPro.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.ClassicControllerPro.RIGHT, Inputs.Xbox360.RIGHT);

                    result.Add(Inputs.ClassicControllerPro.L, Inputs.Xbox360.LB);
                    result.Add(Inputs.ClassicControllerPro.R, Inputs.Xbox360.RB);
                    result.Add(Inputs.ClassicControllerPro.ZL, Inputs.Xbox360.LT);
                    result.Add(Inputs.ClassicControllerPro.ZR, Inputs.Xbox360.RT);

                    result.Add(Inputs.ClassicControllerPro.LUP, Inputs.Xbox360.LUP);
                    result.Add(Inputs.ClassicControllerPro.LDOWN, Inputs.Xbox360.LDOWN);
                    result.Add(Inputs.ClassicControllerPro.LLEFT, Inputs.Xbox360.LLEFT);
                    result.Add(Inputs.ClassicControllerPro.LRIGHT, Inputs.Xbox360.LRIGHT);

                    result.Add(Inputs.ClassicControllerPro.RUP, Inputs.Xbox360.RUP);
                    result.Add(Inputs.ClassicControllerPro.RDOWN, Inputs.Xbox360.RDOWN);
                    result.Add(Inputs.ClassicControllerPro.RLEFT, Inputs.Xbox360.RLEFT);
                    result.Add(Inputs.ClassicControllerPro.RRIGHT, Inputs.Xbox360.RRIGHT);

                    result.Add(Inputs.ClassicControllerPro.SELECT, Inputs.Xbox360.BACK);
                    result.Add(Inputs.ClassicControllerPro.START, Inputs.Xbox360.START);
                    result.Add(Inputs.ClassicControllerPro.HOME, Inputs.Xbox360.GUIDE);

                    result.Add(Inputs.Wiimote.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.Wiimote.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.Wiimote.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.Wiimote.RIGHT, Inputs.Xbox360.RIGHT);
                    result.Add(Inputs.Wiimote.A, Inputs.Xbox360.A);
                    result.Add(Inputs.Wiimote.B, Inputs.Xbox360.B);
                    result.Add(Inputs.Wiimote.ONE, Inputs.Xbox360.LS);
                    result.Add(Inputs.Wiimote.TWO, Inputs.Xbox360.RS);
                    result.Add(Inputs.Wiimote.PLUS, Inputs.Xbox360.BACK);
                    result.Add(Inputs.Wiimote.MINUS, Inputs.Xbox360.START);
                    result.Add(Inputs.Wiimote.HOME, Inputs.Xbox360.GUIDE);
                    result.Add(Inputs.Wiimote.ACC_SHAKE_X, "");
                    result.Add(Inputs.Wiimote.ACC_SHAKE_Y, "");
                    result.Add(Inputs.Wiimote.ACC_SHAKE_Z, "");
                    result.Add(Inputs.Wiimote.TILT_RIGHT, "");
                    result.Add(Inputs.Wiimote.TILT_LEFT, "");
                    result.Add(Inputs.Wiimote.TILT_UP, "");
                    result.Add(Inputs.Wiimote.TILT_DOWN, "");
                    break;

                case ControllerType.ClassicController:
                    result.Add(Inputs.ClassicController.B, Inputs.Xbox360.B);
                    result.Add(Inputs.ClassicController.A, Inputs.Xbox360.A);
                    result.Add(Inputs.ClassicController.Y, Inputs.Xbox360.X);
                    result.Add(Inputs.ClassicController.X, Inputs.Xbox360.Y);

                    result.Add(Inputs.ClassicController.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.ClassicController.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.ClassicController.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.ClassicController.RIGHT, Inputs.Xbox360.RIGHT);

                    result.Add(Inputs.ClassicController.ZL, Inputs.Xbox360.LB);
                    result.Add(Inputs.ClassicController.ZR, Inputs.Xbox360.RB);
                    result.Add(Inputs.ClassicController.LT, Inputs.Xbox360.LT);
                    result.Add(Inputs.ClassicController.RT, Inputs.Xbox360.RT);

                    result.Add(Inputs.ClassicController.LUP, Inputs.Xbox360.LUP);
                    result.Add(Inputs.ClassicController.LDOWN, Inputs.Xbox360.LDOWN);
                    result.Add(Inputs.ClassicController.LLEFT, Inputs.Xbox360.LLEFT);
                    result.Add(Inputs.ClassicController.LRIGHT, Inputs.Xbox360.LRIGHT);

                    result.Add(Inputs.ClassicController.RUP, Inputs.Xbox360.RUP);
                    result.Add(Inputs.ClassicController.RDOWN, Inputs.Xbox360.RDOWN);
                    result.Add(Inputs.ClassicController.RLEFT, Inputs.Xbox360.RLEFT);
                    result.Add(Inputs.ClassicController.RRIGHT, Inputs.Xbox360.RRIGHT);

                    result.Add(Inputs.ClassicController.SELECT, Inputs.Xbox360.BACK);
                    result.Add(Inputs.ClassicController.START, Inputs.Xbox360.START);
                    result.Add(Inputs.ClassicController.HOME, Inputs.Xbox360.GUIDE);

                    result.Add(Inputs.Wiimote.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.Wiimote.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.Wiimote.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.Wiimote.RIGHT, Inputs.Xbox360.RIGHT);
                    result.Add(Inputs.Wiimote.A, Inputs.Xbox360.A);
                    result.Add(Inputs.Wiimote.B, Inputs.Xbox360.B);
                    result.Add(Inputs.Wiimote.ONE, Inputs.Xbox360.LS);
                    result.Add(Inputs.Wiimote.TWO, Inputs.Xbox360.RS);
                    result.Add(Inputs.Wiimote.PLUS, Inputs.Xbox360.BACK);
                    result.Add(Inputs.Wiimote.MINUS, Inputs.Xbox360.START);
                    result.Add(Inputs.Wiimote.HOME, Inputs.Xbox360.GUIDE);
                    result.Add(Inputs.Wiimote.ACC_SHAKE_X, "");
                    result.Add(Inputs.Wiimote.ACC_SHAKE_Y, "");
                    result.Add(Inputs.Wiimote.ACC_SHAKE_Z, "");
                    result.Add(Inputs.Wiimote.TILT_RIGHT, "");
                    result.Add(Inputs.Wiimote.TILT_LEFT, "");
                    result.Add(Inputs.Wiimote.TILT_UP, "");
                    result.Add(Inputs.Wiimote.TILT_DOWN, "");
                    break;

                //case ControllerType.Nunchuk:
                //case ControllerType.NunchukB:
                //    result.Add(Inputs.Nunchuk.UP,    Inputs.Xbox360.LUP);
                //    result.Add(Inputs.Nunchuk.DOWN,  Inputs.Xbox360.LDOWN);
                //    result.Add(Inputs.Nunchuk.LEFT,  Inputs.Xbox360.LLEFT);
                //    result.Add(Inputs.Nunchuk.RIGHT, Inputs.Xbox360.LRIGHT);
                //    result.Add(Inputs.Nunchuk.Z,     Inputs.Xbox360.RT);
                //    result.Add(Inputs.Nunchuk.C,     Inputs.Xbox360.LT);
                //    result.Add(Inputs.Nunchuk.TILT_RIGHT, "");
                //    result.Add(Inputs.Nunchuk.TILT_LEFT, "");
                //    result.Add(Inputs.Nunchuk.TILT_UP, "");
                //    result.Add(Inputs.Nunchuk.TILT_DOWN, "");
                //    result.Add(Inputs.Nunchuk.ACC_SHAKE_X, "");
                //    result.Add(Inputs.Nunchuk.ACC_SHAKE_Y, "");
                //    result.Add(Inputs.Nunchuk.ACC_SHAKE_Z, "");

                //    result.Add(Inputs.Wiimote.UP,    Inputs.Xbox360.UP);
                //    result.Add(Inputs.Wiimote.DOWN,  Inputs.Xbox360.DOWN);
                //    result.Add(Inputs.Wiimote.LEFT,  Inputs.Xbox360.LB);
                //    result.Add(Inputs.Wiimote.RIGHT, Inputs.Xbox360.RB);
                //    result.Add(Inputs.Wiimote.A,     Inputs.Xbox360.A);
                //    result.Add(Inputs.Wiimote.B,     Inputs.Xbox360.B);
                //    result.Add(Inputs.Wiimote.ONE,   Inputs.Xbox360.X);
                //    result.Add(Inputs.Wiimote.TWO,   Inputs.Xbox360.Y);
                //    result.Add(Inputs.Wiimote.PLUS,  Inputs.Xbox360.BACK);
                //    result.Add(Inputs.Wiimote.MINUS, Inputs.Xbox360.START);
                //    result.Add(Inputs.Wiimote.HOME,  Inputs.Xbox360.GUIDE);
                //    result.Add(Inputs.Wiimote.ACC_SHAKE_X, "");
                //    result.Add(Inputs.Wiimote.ACC_SHAKE_Y, "");
                //    result.Add(Inputs.Wiimote.ACC_SHAKE_Z, "");
                //    result.Add(Inputs.Wiimote.TILT_RIGHT, "");
                //    result.Add(Inputs.Wiimote.TILT_LEFT, "");
                //    result.Add(Inputs.Wiimote.TILT_UP, "");
                //    result.Add(Inputs.Wiimote.TILT_DOWN, "");
                //    break;

                case ControllerType.Nunchuk:
                case ControllerType.NunchukB:
                case ControllerType.Wiimote:
                    result.Add(Inputs.Wiimote.RIGHT, Inputs.Xbox360.UP);
                    result.Add(Inputs.Wiimote.LEFT, Inputs.Xbox360.DOWN);

                    result.Add(Inputs.Wiimote.B, Inputs.Xbox360.A); //Green
                    result.Add(Inputs.Wiimote.DOWN, Inputs.Xbox360.B); //Red
                    result.Add(Inputs.Wiimote.A, Inputs.Xbox360.Y); //Yellow
                    result.Add(Inputs.Wiimote.ONE, Inputs.Xbox360.X); //Blue
                    result.Add(Inputs.Wiimote.TWO, Inputs.Xbox360.LB); //Orange

                    result.Add(Inputs.Wiimote.UP, Inputs.Xbox360.BACK); //SP

                    result.Add(Inputs.Wiimote.PLUS, Inputs.Xbox360.START);
                    result.Add(Inputs.Wiimote.MINUS, Inputs.Xbox360.BACK);
                    result.Add(Inputs.Wiimote.HOME, Inputs.Xbox360.GUIDE);

                    //result.Add(Inputs.Wiimote.LEFT,  Inputs.Xbox360.DOWN);






                    result.Add(Inputs.Wiimote.ACC_SHAKE_X, Inputs.Xbox360.RRIGHT);
                    result.Add(Inputs.Wiimote.ACC_SHAKE_Y, Inputs.Xbox360.RRIGHT);
                    result.Add(Inputs.Wiimote.ACC_SHAKE_Z, Inputs.Xbox360.RRIGHT);
                    result.Add(Inputs.Wiimote.TILT_RIGHT, "");
                    result.Add(Inputs.Wiimote.TILT_LEFT, "");
                    result.Add(Inputs.Wiimote.TILT_UP, "");
                    result.Add(Inputs.Wiimote.TILT_DOWN, "");
                    result.Add(Inputs.Wiimote.IR_RIGHT, "");
                    result.Add(Inputs.Wiimote.IR_LEFT, "");
                    result.Add(Inputs.Wiimote.IR_UP, "");
                    result.Add(Inputs.Wiimote.IR_DOWN, "");
                    break;

                case ControllerType.Guitar:

                    result.Add(Inputs.Guitar.G, Inputs.Xbox360.A);
                    result.Add(Inputs.Guitar.R, Inputs.Xbox360.B);
                    result.Add(Inputs.Guitar.Y, Inputs.Xbox360.Y);
                    result.Add(Inputs.Guitar.B, Inputs.Xbox360.X);
                    result.Add(Inputs.Guitar.O, Inputs.Xbox360.LB);

                    result.Add(Inputs.Guitar.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.Guitar.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.Guitar.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.Guitar.RIGHT, Inputs.Xbox360.RIGHT);

                    result.Add(Inputs.Guitar.SELECT, Inputs.Xbox360.BACK);
                    result.Add(Inputs.Guitar.START, Inputs.Xbox360.START);
                    result.Add(Inputs.Guitar.HOME, Inputs.Xbox360.GUIDE);

                    result.Add(Inputs.Guitar.WHAMMYLOW, Inputs.Xbox360.RLEFT);
                    result.Add(Inputs.Guitar.WHAMMYHIGH, Inputs.Xbox360.RRIGHT);

                    result.Add(Inputs.Guitar.TILTLOW, Inputs.Xbox360.RDOWN);
                    result.Add(Inputs.Guitar.TILTHIGH, Inputs.Xbox360.RUP);

                    //result.Add(Inputs.Wiimote.UP, "");
                    //result.Add(Inputs.Wiimote.DOWN, "");
                    //result.Add(Inputs.Wiimote.LEFT, "");
                    //result.Add(Inputs.Wiimote.RIGHT, "");
                    //result.Add(Inputs.Wiimote.A, "");
                    //result.Add(Inputs.Wiimote.B, "");
                    //result.Add(Inputs.Wiimote.ONE, "");
                    //result.Add(Inputs.Wiimote.TWO, "");
                    //result.Add(Inputs.Wiimote.PLUS, "");
                    //result.Add(Inputs.Wiimote.MINUS, "");
                    //result.Add(Inputs.Wiimote.HOME, "");
                    //result.Add(Inputs.Wiimote.ACC_SHAKE_X, "");
                    //result.Add(Inputs.Wiimote.ACC_SHAKE_Y, "");
                    //result.Add(Inputs.Wiimote.ACC_SHAKE_Z, "");
                    //result.Add(Inputs.Wiimote.TILT_RIGHT, "");
                    //result.Add(Inputs.Wiimote.TILT_LEFT, "");
                    //result.Add(Inputs.Wiimote.TILT_UP, "");
                    //result.Add(Inputs.Wiimote.TILT_DOWN, "");
                    //result.Add(Inputs.Wiimote.IR_RIGHT, "");
                    //result.Add(Inputs.Wiimote.IR_LEFT, "");
                    //result.Add(Inputs.Wiimote.IR_UP, "");
                    //result.Add(Inputs.Wiimote.IR_DOWN, "");

                    break;

                case ControllerType.Drums:
                    result.Add(Inputs.Drums.G, Inputs.Xbox360.A);
                    result.Add(Inputs.Drums.R, Inputs.Xbox360.B);
                    result.Add(Inputs.Drums.Y, Inputs.Xbox360.Y);
                    result.Add(Inputs.Drums.B, Inputs.Xbox360.X);
                    result.Add(Inputs.Drums.O, Inputs.Xbox360.RB);
                    result.Add(Inputs.Drums.BASS, Inputs.Xbox360.LB);

                    result.Add(Inputs.Drums.UP, Inputs.Xbox360.UP);
                    result.Add(Inputs.Drums.DOWN, Inputs.Xbox360.DOWN);
                    result.Add(Inputs.Drums.LEFT, Inputs.Xbox360.LEFT);
                    result.Add(Inputs.Drums.RIGHT, Inputs.Xbox360.RIGHT);

                    result.Add(Inputs.Drums.SELECT, Inputs.Xbox360.BACK);
                    result.Add(Inputs.Drums.START, Inputs.Xbox360.START);
                    result.Add(Inputs.Drums.HOME, Inputs.Xbox360.GUIDE);
                    break;
            }

            return result;
        }

        public XInputHolder()
        {
#if MouseMode
            _inputSim = new WindowsInput.InputSimulator();
#endif
            //Values = new Dictionary<string, float>();
            Values = new System.Collections.Concurrent.ConcurrentDictionary<string, float>();
            Mappings = new Dictionary<string, string>();
            Flags = new Dictionary<string, bool>();
            ResetReport();

            //if (!Flags.ContainsKey(Inputs.Flags.RUMBLE))
            //{
            //    Flags.Add(Inputs.Flags.RUMBLE, false);
            //}
            //
            //if (!Values.ContainsKey(Inputs.Flags.RUMBLE))
            //{
            //    Values.TryAdd(Inputs.Flags.RUMBLE, 0f);
            //}
        }

        private void ResetReport()
        {
            writeReport = new Dictionary<string, float>()
            {
                {Inputs.Xbox360.A, 0},
                {Inputs.Xbox360.B, 0},
                {Inputs.Xbox360.X, 0},
                {Inputs.Xbox360.Y, 0},
                {Inputs.Xbox360.UP, 0},
                {Inputs.Xbox360.DOWN, 0},
                {Inputs.Xbox360.LEFT, 0},
                {Inputs.Xbox360.RIGHT, 0},
                {Inputs.Xbox360.LB, 0},
                {Inputs.Xbox360.RB, 0},
                {Inputs.Xbox360.BACK, 0},
                {Inputs.Xbox360.START, 0},
                {Inputs.Xbox360.GUIDE, 0},
                {Inputs.Xbox360.LS, 0},
                {Inputs.Xbox360.RS, 0},
            };
        }

        public XInputHolder(ControllerType t) : this()
        {
            Mappings = GetDefaultMapping(t);
        }

        public override void Update()
        {
            if (!connected)
            {
                return;
            }

#if MouseMode
            if (InMouseMode)
            {
                UpdateMouseMode();
                return;
            }
#endif

            byte[] rumble = new byte[8];
            byte[] report = new byte[28];
            byte[] parsed = new byte[28];

            #region Populate Report Revised
            report[0] = (byte)ID;
            report[1] = 0x02;

            report[10] = 0;
            report[11] = 0;
            report[12] = 0;
            report[13] = 0;

            float LX = 0f;
            float LY = 0f;
            float RX = 0f;
            float RY = 0f;

            float LT = 0f;
            float RT = 0f;

            ResetReport();

            //foreach (KeyValuePair<string, string> map in Mappings)
            for (int i = 0; i < Mappings.Count; i++)
            {
                var map = Mappings.ElementAt(i);

                if (!Values.ContainsKey(map.Key))
                {
                    continue;
                }

                if (writeReport.ContainsKey(map.Value))
                {
                    try
                    {
                        writeReport[map.Value] += Values[map.Key];
                    }
                    catch (KeyNotFoundException) { }
                }
                else if (Values.ContainsKey(map.Key))
                {
                    switch (map.Value)
                    {
                        case Inputs.Xbox360.LLEFT: try { LX -= Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.LRIGHT: try { LX += Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.LUP: try { LY += Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.LDOWN: try { LY -= Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.RLEFT: try { RX -= Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.RRIGHT: try { RX += Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.RUP: try { RY += Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.RDOWN: try { RY -= Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.LT: try { LT += Values[map.Key]; } catch { } break;
                        case Inputs.Xbox360.RT: try { RT += Values[map.Key]; } catch { } break;

#if MouseMode
                        case "MouseMode": 
                            if (Values[map.Key] > 0f && DateTime.Now.Subtract(_mmLastTime).TotalSeconds > 3)
                            {
                                _mmLastTime = DateTime.Now;
                                InMouseMode = true;
                            }
                            break;
#endif
                    }
                }
            }

            report[10] |= (byte)(writeReport[Inputs.Xbox360.BACK] > 0f ? 1 << 0 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.LS] > 0f ? 1 << 1 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.RS] > 0f ? 1 << 2 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.START] > 0f ? 1 << 3 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.UP] > 0f ? 1 << 4 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.DOWN] > 0f ? 1 << 5 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.RIGHT] > 0f ? 1 << 6 : 0);
            report[10] |= (byte)(writeReport[Inputs.Xbox360.LEFT] > 0f ? 1 << 7 : 0);

            report[11] |= (byte)(writeReport[Inputs.Xbox360.LB] > 0f ? 1 << 2 : 0);
            report[11] |= (byte)(writeReport[Inputs.Xbox360.RB] > 0f ? 1 << 3 : 0);
            report[11] |= (byte)(writeReport[Inputs.Xbox360.Y] > 0f ? 1 << 4 : 0);
            report[11] |= (byte)(writeReport[Inputs.Xbox360.B] > 0f ? 1 << 5 : 0);
            report[11] |= (byte)(writeReport[Inputs.Xbox360.A] > 0f ? 1 << 6 : 0);
            report[11] |= (byte)(writeReport[Inputs.Xbox360.X] > 0f ? 1 << 7 : 0);

            report[12] |= (byte)(writeReport[Inputs.Xbox360.GUIDE] > 0f ? 1 << 0 : 0);

            report[14] = (byte)((GetRawAxis(LX) >> 0) & 0xFF);
            report[15] = (byte)((GetRawAxis(LX) >> 8) & 0xFF);
            report[16] = (byte)((GetRawAxis(LY) >> 0) & 0xFF);
            report[17] = (byte)((GetRawAxis(LY) >> 8) & 0xFF);
            report[18] = (byte)((GetRawAxis(RX) >> 0) & 0xFF);
            report[19] = (byte)((GetRawAxis(RX) >> 8) & 0xFF);
            report[20] = (byte)((GetRawAxis(RY) >> 0) & 0xFF);
            report[21] = (byte)((GetRawAxis(RY) >> 8) & 0xFF);

            report[26] = GetRawTrigger(LT);
            report[27] = GetRawTrigger(RT);
            #endregion

            bus.Parse(report, parsed);

            if (bus.Report(parsed, rumble))
            {
                // This is set on a rumble state change
                if (rumble[1] == 0x08)
                {
                    // Check if it's strong enough to rumble
                    int strength = BitConverter.ToInt32(new byte[] { rumble[4], rumble[3], 0x00, 0x00 }, 0);
                    //System.Diagnostics.Debug.WriteLine(strength);
                    Flags[Inputs.Flags.RUMBLE] = (strength > minRumble);
                    //Values[Inputs.Flags.RUMBLE] = strength > minRumble ? strength : 0;
                    RumbleAmount = strength > minRumble ? strength : 0;
                }
            }
        }

        public override void Close()
        {
            Flags[Inputs.Flags.RUMBLE] = false;
            bus.Unplug(ID);

            if (ID > -1 && ID < 4)
            {
                availabe[ID] = true;
            }

            ID = -1;
            connected = false;
        }

        public override void AddMapping(ControllerType controller)
        {
            var additional = GetDefaultMapping(controller);

            foreach (KeyValuePair<string, string> map in additional)
            {
                if (!Mappings.ContainsKey(map.Key) && map.Key[0] != 'w')
                {
                    SetMapping(map.Key, map.Value);
                }
            }
        }

        public bool ConnectXInput(int id)
        {
            if (id > -1 && id < 4)
            {
                availabe[id] = false;
            }
            else
            {
                return false;
            }

            bus = XBus.Default;
            bus.Unplug(id);
            bus.Plugin(id);
            ID = id;
            connected = true;
            return true;
        }

        public bool RemoveXInput(int id)
        {
            if (id > -1 && id < 4)
            {
                availabe[id] = true;
            }


            Flags[Inputs.Flags.RUMBLE] = false;
            if (bus.Unplug(id))
            {
                ID = -1;
                connected = false;
                return true;
            }

            return false;
        }

        public Int32 GetRawAxis(double axis)
        {
            if (axis > 1.0)
            {
                return 32767;
            }
            if (axis < -1.0)
            {
                return -32767;
            }

            return (Int32)(axis * 32767);
        }

        public byte GetRawTrigger(double trigger)
        {
            if (trigger > 1.0)
            {
                return 255;
            }
            if (trigger < 0.0)
            {
                return 0;
            }

            return (Byte)(trigger * 255);
        }
    }

    public class XBus
    {
        private static XBus defaultInstance;
        private ViGEmClient viGEmClient;
        private Dictionary<int, IXbox360Controller> targets;
        private List<IXbox360Controller> connected;

        // Default Bus
        public static XBus Default
        {
            get
            {
                // if it hasn't been created create one
                if (defaultInstance == null)
                {
                    defaultInstance = new XBus();
                }

                return defaultInstance;
            }
        }

        public ViGEmClient Client
        {
            get
            {
                return viGEmClient;
            }
            private set
            {
                viGEmClient = value;
            }
        }

        public XBus()
        {
            Client = new ViGEmClient();
            targets = new Dictionary<int, IXbox360Controller>();
            connected = new List<IXbox360Controller>();
            App.Current.Exit += StopDevice;
        }

        private void StopDevice(object sender, System.Windows.ExitEventArgs e)
        {
            if (defaultInstance != null)
            {
                foreach (IXbox360Controller controller in targets.Values)
                {
                    if (connected.Contains(controller))
                    {
                        controller.Disconnect();
                        connected.Remove(controller);
                    }
                }
                Client.Dispose();
            }
        }

        public void Plugin(int id, ushort vid = 0, ushort pid = 0)
        {
            if (vid != 0 && pid != 0)
            {
                targets.Add(id, Client.CreateXbox360Controller(vid, pid));
            }
            else
            {
                targets.Add(id, Client.CreateXbox360Controller());
            }
            targets[id].AutoSubmitReport = false;
            targets[id].Connect();
            connected.Add(targets[id]);
        }

        public bool Unplug(int id)
        {
            if (targets.Count > id && targets[id] != null)
            {
                if (connected.Contains(targets[id]))
                {
                    targets[id].Disconnect();
                    connected.Remove(targets[id]);
                    targets.Remove(id);
                    return true;
                }
                return false;
            }

            return false;
        }

        public int Parse(byte[] Input, byte[] Output)
        {
            for (int index = 0; index < 28; index++)
            {
                Output[index] = 0x00;
            }

            Output[0] = 0x1C;
            Output[4] = Input[0];
            Output[9] = 0x14;

            if (Input[1] == 0x02) // Pad is active
            {
                UInt32 Buttons = (UInt32)((Input[10] << 0) | (Input[11] << 8) | (Input[12] << 16) | (Input[13] << 24));

                if ((Buttons & (0x1 << 0)) > 0) Output[10] |= (Byte)(1 << 5); // Back
                if ((Buttons & (0x1 << 1)) > 0) Output[10] |= (Byte)(1 << 6); // Left  Thumb
                if ((Buttons & (0x1 << 2)) > 0) Output[10] |= (Byte)(1 << 7); // Right Thumb
                if ((Buttons & (0x1 << 3)) > 0) Output[10] |= (Byte)(1 << 4); // Start

                if ((Buttons & (0x1 << 4)) > 0) Output[10] |= (Byte)(1 << 0); // Up
                if ((Buttons & (0x1 << 5)) > 0) Output[10] |= (Byte)(1 << 1); // Down
                if ((Buttons & (0x1 << 6)) > 0) Output[10] |= (Byte)(1 << 3); // Right
                if ((Buttons & (0x1 << 7)) > 0) Output[10] |= (Byte)(1 << 2); // Left

                if ((Buttons & (0x1 << 10)) > 0) Output[11] |= (Byte)(1 << 0); // Left  Shoulder
                if ((Buttons & (0x1 << 11)) > 0) Output[11] |= (Byte)(1 << 1); // Right Shoulder

                if ((Buttons & (0x1 << 12)) > 0) Output[11] |= (Byte)(1 << 7); // Y
                if ((Buttons & (0x1 << 13)) > 0) Output[11] |= (Byte)(1 << 5); // B
                if ((Buttons & (0x1 << 14)) > 0) Output[11] |= (Byte)(1 << 4); // A
                if ((Buttons & (0x1 << 15)) > 0) Output[11] |= (Byte)(1 << 6); // X

                if ((Buttons & (0x1 << 16)) > 0) Output[11] |= (Byte)(1 << 2); // Guide

                Output[12] = Input[26]; // Left Trigger
                Output[13] = Input[27]; // Right Trigger

                Output[14] = Input[14]; // LX
                Output[15] = Input[15];

                Output[16] = Input[16]; // LY
                Output[17] = Input[17];

                Output[18] = Input[18]; // RX
                Output[19] = Input[19];

                Output[20] = Input[20]; // RY
                Output[21] = Input[21];
            }

            return Input[0];
        }

        public virtual bool Report(byte[] input, byte[] output)
        {
            byte id = (byte)(input[4]);
            if (targets.Count <= id || targets[id] == null)
            {
                return false;
            }
            IXbox360Controller controller = targets[id];
            controller.ResetReport();
            if ((input[10] & 32) != 0)
                controller.SetButtonState(Xbox360Button.Back, true);
            if ((input[10] & 64) != 0)
                controller.SetButtonState(Xbox360Button.LeftThumb, true);
            if ((input[10] & 128) != 0)
                controller.SetButtonState(Xbox360Button.RightThumb, true);
            if ((input[10] & 16) != 0)
                controller.SetButtonState(Xbox360Button.Start, true);
            if ((input[10] & 1) != 0)
                controller.SetButtonState(Xbox360Button.Up, true);
            if ((input[10] & 2) != 0)
                controller.SetButtonState(Xbox360Button.Down, true);
            if ((input[10] & 4) != 0)
                controller.SetButtonState(Xbox360Button.Left, true);
            if ((input[10] & 8) != 0)
                controller.SetButtonState(Xbox360Button.Right, true);

            if ((input[11] & 1) != 0)
                controller.SetButtonState(Xbox360Button.LeftShoulder, true);
            if ((input[11] & 2) != 0)
                controller.SetButtonState(Xbox360Button.RightShoulder, true);
            if ((input[11] & 128) != 0)
                controller.SetButtonState(Xbox360Button.Y, true);
            if ((input[11] & 32) != 0)
                controller.SetButtonState(Xbox360Button.B, true);
            if ((input[11] & 16) != 0)
                controller.SetButtonState(Xbox360Button.A, true);
            if ((input[11] & 64) != 0)
                controller.SetButtonState(Xbox360Button.X, true);
            if ((input[11] & 4) != 0)
                controller.SetButtonState(Xbox360Button.Guide, true);

            controller.SetSliderValue(Xbox360Slider.LeftTrigger, input[12]);
            controller.SetSliderValue(Xbox360Slider.RightTrigger, input[13]);

            controller.SetAxisValue(Xbox360Axis.LeftThumbX, BitConverter.ToInt16(input, 14));
            controller.SetAxisValue(Xbox360Axis.LeftThumbY, BitConverter.ToInt16(input, 16));
            controller.SetAxisValue(Xbox360Axis.RightThumbX, BitConverter.ToInt16(input, 18));
            controller.SetAxisValue(Xbox360Axis.RightThumbY, BitConverter.ToInt16(input, 20));

            controller.SubmitReport();
            return true;
        }
    }
}
