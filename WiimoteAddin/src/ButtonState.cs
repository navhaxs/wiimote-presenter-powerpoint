using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiimoteLib;

namespace WiimoteAddin
{

    // This class handles the Wiimote button state changes
    // (on button press, on button release)
    class ButtonState
    {

        public void Update(WiimoteState ws)
        {
            // todo: reduce redundancy
            if (ws.ButtonState.A != A)
            {
                if (ws.ButtonState.A == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.A, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.A, true);
                }
            }
            if (ws.ButtonState.B != B)
            {
                if (ws.ButtonState.B == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.B, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.B, true);
                }
            }
            if (ws.ButtonState.Up != Up)
            {
                if (ws.ButtonState.Up == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Up, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Up, true);
                }
            }
            if (ws.ButtonState.Down != Down)
            {
                if (ws.ButtonState.Down == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Down, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Down, true);
                }
            }
            if (ws.ButtonState.Left != Left)
            {
                if (ws.ButtonState.Left == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Left, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Left, true);
                }
            }
            if (ws.ButtonState.Right != Right)
            {
                if (ws.ButtonState.Right == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Right, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Right, true);
                }
            }
            if (ws.ButtonState.Home != Home)
            {
                if (ws.ButtonState.Home == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Home, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Home, true);
                }
            }
            if (ws.ButtonState.Plus != Plus)
            {
                if (ws.ButtonState.Plus == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Plus, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Plus, true);
                }
            }
            if (ws.ButtonState.Minus != Minus)
            {
                if (ws.ButtonState.Minus == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Minus, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Minus, true);
                }
            }
            if (ws.ButtonState.One != One)
            {
                if (ws.ButtonState.One == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.One, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.One, true);
                }
            }
            if (ws.ButtonState.Two != Two)
            {
                if (ws.ButtonState.Two == true)
                {
                    // Button Up
                    ReturnStatusUpdate(Button.Two, false);
                }
                else
                {
                    // Button Down
                    ReturnStatusUpdate(Button.Two, true);
                }
            }
            A = ws.ButtonState.A;
            B = ws.ButtonState.B;
            Up = ws.ButtonState.Up;
            Down = ws.ButtonState.Down;
            Left = ws.ButtonState.Left;
            Right = ws.ButtonState.Right;
            Home = ws.ButtonState.Home;
            Plus = ws.ButtonState.Plus;
            Minus = ws.ButtonState.Minus;
            One = ws.ButtonState.One;
            Two = ws.ButtonState.Two;
        }

        private void ReturnStatusUpdate(Button button, bool buttonDown) // to indicate buttonUp, set buttonDown to be false
        {
            if (ButtonStateChanged == null) return;

            ButtonStateChangedArgs args = new ButtonStateChangedArgs(button, buttonDown);
            ButtonStateChanged(this, args);
        }

        public delegate void _ButtonStateChanged(object sender, ButtonStateChangedArgs e);
        public event _ButtonStateChanged ButtonStateChanged;

        // holds the previous states of the buttons
        private bool A = false;
        private bool B = false;
        private bool Up = false;
        private bool Down = false;
        private bool Left = false;
        private bool Right = false;
        private bool Home = false;
        private bool Plus = false;
        private bool Minus = false;
        private bool One = false;
        private bool Two = false;
    }

    public enum Button
    {
        A = 0,
        B  = 1,
        Up = 2,
        Down = 3,
        Left = 4,
        Right = 5, 
        Home = 6,
        Plus = 7,
        Minus = 8,
        One = 9,
        Two = 10
    }

    public class ButtonStateChangedArgs : EventArgs
    {
        public Button button { get; private set; }

        public bool buttonDown { get; private set; }

        public ButtonStateChangedArgs(Button _button, bool _buttonDown)
        {
            button = _button;
            buttonDown = _buttonDown;   
        }
    }
}
