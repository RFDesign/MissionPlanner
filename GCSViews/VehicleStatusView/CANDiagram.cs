using System;
using System.Collections.Generic;
using System.Drawing;

namespace RFD.MonsterCopter.GUI.CANDiagram
{
    public class TDiagram
    {
        public Color BackColour = Color.Black;
        public Font Font;
        static readonly Color ACTIVE_COLOUR = Color.Green;
        /// <summary>
        /// orange
        /// </summary>
        static readonly Color STANDBY_COLOUR = Color.FromArgb(255, 100, 0);
        static readonly Color ARBITER_COLOUR = Color.LightBlue;
        static readonly Color CAN_BUS_COLOUR = Color.White;
        static readonly Color BOX_TEXT_COLOUR = Color.Black;
        const int FC_SIZE = 100;
        const int ARB_SIZE = 100;
        const int SPACING = 20;
        const int MARGIN = 10;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="F">Must not be null.</param>
        public TDiagram(Font F)
        {
            this.Font = F;
        }

        /// <summary>
        /// Draw the diagram described by the given status on the given graphics.
        /// </summary>
        /// <param name="G">Must not be null.</param>
        /// <param name="Status">Must not be null.</param>
        public void Draw(Graphics G, RFD.Arbitration.TStatus Status)
        {
            G.Clear(BackColour);

            Pen P = new Pen(CAN_BUS_COLOUR);

            for (int n = 0; n < Status.FlightControllers.Length; n++)
            {
                int Top = MARGIN + ARB_SIZE + SPACING + n * (FC_SIZE + SPACING);
                DrawFlightController(G, new Point(MARGIN, Top), Status.FlightControllers[n]);
                int LineRight = MARGIN + FC_SIZE + SPACING + (Status.Arbiters.Length - 1) * (ARB_SIZE + SPACING) + ARB_SIZE;
                G.DrawLine(P, MARGIN + FC_SIZE, Top + FC_SIZE / 2, LineRight, Top + FC_SIZE / 2);
                int ArbOffset = GetArbBusOffset(Status.FlightControllers.Length, n);
                for (int m = 0; m < Status.Arbiters.Length; m++)
                {
                    DrawJoint(G, MARGIN + FC_SIZE + SPACING + ArbOffset + (ARB_SIZE + SPACING) * m, Top + FC_SIZE / 2);
                }

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Near;
                SF.LineAlignment = StringAlignment.Center;

                SolidBrush SB = new SolidBrush(CAN_BUS_COLOUR);

                G.DrawString("Bus " + Status.FlightControllers[n].BusID.ToString(), this.Font, SB, LineRight, Top + FC_SIZE / 2, SF);
            }

            for (int n = 0; n < Status.Arbiters.Length; n++)
            {
                int Left = MARGIN + FC_SIZE + SPACING + n * (ARB_SIZE + SPACING);
                DrawArbiter(G, new Point(Left, MARGIN), Status.Arbiters[n]);

                for (int m = 0; m < Status.FlightControllers.Length; m++)
                {
                    int ArbOffset = GetArbBusOffset(Status.FlightControllers.Length, m);
                    int Bottom = MARGIN + ARB_SIZE + SPACING + (m) * (SPACING + FC_SIZE) + FC_SIZE / 2;
                    int Top = MARGIN + ARB_SIZE;
                    G.DrawLine(P, Left + ArbOffset, Top, Left + ArbOffset, Bottom);
                }
            }
        }

        void DrawJoint(Graphics G, int x, int y)
        {
            SolidBrush SB = new SolidBrush(CAN_BUS_COLOUR);
            G.FillEllipse(SB, x - 5, y - 5, 10, 10);
        }


        int GetArbBusOffset(int QTYFC, int FCIndex)
        {
            int Pitch = ARB_SIZE / (2 * QTYFC + 1);
            return Pitch + 2 * FCIndex * Pitch + Pitch / 2;
        }

        static string FunctionalStateToString(RFD.Arbitration.TFunctionalState State)
        {
            switch (State)
            {
                case RFD.Arbitration.TFunctionalState.ACTIVE:
                    return "ACTIVE";
                case RFD.Arbitration.TFunctionalState.STANDBY:
                    return "STANDBY";
                default:
                    return "ERROR";
            }
        }

        static string MillisecondsToHumanReadable(UInt64 Milliseconds)
        {
            if (Milliseconds < 1000)
            {
                return Milliseconds.ToString() + "ms";
            }
            else if (Milliseconds < 60000)
            {
                return (Milliseconds / 1000).ToString() + "s";
            }
            else
            {
                UInt64 Minutes = Milliseconds / 60000;
                UInt64 Seconds = (Milliseconds - (Minutes * 60000)) / 1000;

                return Minutes.ToString() + "m" + Seconds.ToString() + "s";
            }
        }

        static Color FunctionalStateToColour(RFD.Arbitration.TFunctionalState FS)
        {
            switch (FS)
            {
                case RFD.Arbitration.TFunctionalState.ACTIVE:
                    return ACTIVE_COLOUR;
                case RFD.Arbitration.TFunctionalState.STANDBY:
                    return STANDBY_COLOUR;
                default:
                    return Color.Black;
            }
        }

        void AddStringIfTimeKnown(List<string> Strings, UInt64 Time, Func<UInt64, string> TimeToStringFn)
        {
            if (Time != RFD.Arbitration.TStatus.TFlightController.TIME_UNKNOWN)
            {
                Strings.Add(TimeToStringFn(Time));
            }
        }

        void DrawFlightController(Graphics G, Point TopLeft, RFD.Arbitration.TStatus.TFlightController FC)
        {
            List<string> Strings = new List<string>();
            Strings.Add("Flight Controller");
            Strings.Add(FunctionalStateToString(FC.FunctionalState));
            AddStringIfTimeKnown(Strings, FC.TimeInState, (t) => "for " + MillisecondsToHumanReadable(t));
            if (!float.IsNaN(FC.ErrorScore))
            {
                Strings.Add("EKF Error score: " + FC.ErrorScore.ToString());
            }
            AddStringIfTimeKnown(Strings, FC.UpTime, (t) => "Up time: " + MillisecondsToHumanReadable(t));

            DrawBox(G, TopLeft, FunctionalStateToColour(FC.FunctionalState), Strings.ToArray(), FC_SIZE);
        }

        void DrawArbiter(Graphics G, Point TopLeft, RFD.Arbitration.TStatus.TArbiter Arb)
        {
            string[] Text = new string[2];
            Text[0] = "Arbiter";
            Text[1] = Arb.UniqueID.ToString("X");

            DrawBox(G, TopLeft, ARBITER_COLOUR, Text, ARB_SIZE);

            int DeemingSize = ARB_SIZE / (2 * Arb.Deeming.Length + 1);
            for (int n = 0; n < Arb.Deeming.Length; n++)
            {
                SolidBrush SB = new SolidBrush(FunctionalStateToColour(Arb.Deeming[n]));
                G.FillRectangle(SB, TopLeft.X + DeemingSize + 2 * DeemingSize * n, TopLeft.Y + ARB_SIZE - DeemingSize, DeemingSize, DeemingSize);
            }
        }

        Color ReduceBrightness(Color C, float ByFraction)
        {
            float Coeff = 1 - ByFraction;

            Color Result = Color.FromArgb((int)(C.R * Coeff), (int)(C.G * Coeff), (int)(C.B * Coeff));
            return Result;
        }

        void DrawBox(Graphics G, Point TopLeft, Color Colour, string[] Text, int Size)
        {
            //SolidBrush SB = new SolidBrush(Colour);
            System.Drawing.Drawing2D.LinearGradientBrush LGB = new System.Drawing.Drawing2D.LinearGradientBrush(TopLeft, 
                TopLeft + new Size(Size, Size), Colour, ReduceBrightness(Colour, 0.5f));
            StringFormat SF = new StringFormat();
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            G.FillRectangle(LGB, TopLeft.X, TopLeft.Y, Size, Size);

            int TextLineSpacing = Size / (Text.Length + 1);
            SolidBrush TB = new SolidBrush(BOX_TEXT_COLOUR);
            for (int n = 0; n < Text.Length; n++)
            {
                G.DrawString(Text[n], Font, TB, TopLeft.X + Size / 2, TopLeft.Y + (n + 1) * TextLineSpacing, SF);
            }
        }
    }
}