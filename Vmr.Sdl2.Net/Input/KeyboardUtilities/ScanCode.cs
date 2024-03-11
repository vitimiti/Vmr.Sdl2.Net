// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

public enum ScanCode
{
    Unknown = 0x0000,
    A = 0x0004,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,
    Number1,
    Number2,
    Number3,
    Number4,
    Number5,
    Number6,
    Number7,
    Number8,
    Number9,
    Number0,
    Return,
    Escape,
    Backspace,
    Tab,
    Space,
    Minus,
    EqualsCode,
    LeftBracket,
    RightBracket,
    Backslash,
    NonUsHash,
    Semicolon,
    Apostrophe,
    Grave,
    Comma,
    Period,
    Slash,
    CapsLock,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    PrintScreen,
    ScrollLock,
    Pause,
    Insert,
    Home,
    PageUp,
    Delete,
    End,
    PageDown,
    Right,
    Left,
    Down,
    Up,
    NumLockClear,
    KeypadDivide,
    KeypadMultiply,
    KeypadMinus,
    KeypadPlus,
    KeypadEnter,
    KeypadNumber1,
    KeypadNumber2,
    KeypadNumber3,
    KeypadNumber4,
    KeypadNumber5,
    KeypadNumber6,
    KeypadNumber7,
    KeypadNumber8,
    KeypadNumber9,
    KeypadNumber0,
    KeypadPeriod,
    NonUsBackslash,
    Application,
    Power,
    KeypadEquals,
    F13,
    F14,
    F15,
    F16,
    F17,
    F18,
    F19,
    F20,
    F21,
    F22,
    F23,
    F24,
    Execute,
    Help,
    Menu,
    Select,
    Stop,
    Again,
    Undo,
    Cut,
    Copy,
    Paste,
    Find,
    Mute,
    VolumeUp,
    VolumeDown,
    KeypadComma = 0x0085,
    KeypadEqualsAs400,
    International1,
    International2,
    International3,
    International4,
    International5,
    International6,
    International7,
    International8,
    International9,
    Lang1,
    Lang2,
    Lang3,
    Lang4,
    Lang5,
    Lang6,
    Lang7,
    Lang8,
    Lang9,
    AltErase,
    SysReq,
    Cancel,
    Clear,
    Prior,
    Return2,
    Separator,
    Out,
    Oper,
    ClearAgain,
    CrSel,
    ExSel,
    KeypadNumber00 = 0x00B0,
    KeypadNumber000,
    ThousandsSeparator,
    DecimalSeparator,
    CurrencyUnit,
    CurrencySubunit,
    KeypadLeftParenthesis,
    KeypadRightParenthesis,
    KeypadLeftBrace,
    KeypadRightBrace,
    KeypadTab,
    KeypadBackspace,
    KeypadA,
    KeypadB,
    KeypadC,
    KeypadD,
    KeypadE,
    KeypadF,
    KeypadXor,
    KeypadPower,
    KeypadPercent,
    KeypadLess,
    KeypadGreater,
    KeypadAmpersand,
    KeypadDoubleAmpersand,
    KeypadVerticalBar,
    KeypadDoubleVerticalBar,
    KeypadColon,
    KeypadHash,
    KeypadSpace,
    KeypadAt,
    KeypadExclamation,
    KeypadMemoryStore,
    KeypadMemoryRecall,
    KeypadMemoryClear,
    KeypadMemoryAdd,
    KeypadMemorySubtract,
    KeypadMemoryMultiply,
    KeypadMemoryDivide,
    KeypadPlusMinus,
    KeypadClear,
    KeypadClearEntry,
    KeypadBinary,
    KeypadOctal,
    KeypadDecimal,
    KeypadHexadecimal,
    LeftCtrl = 0x00E0,
    LeftShift,
    LeftAlt,
    LeftGui,
    RightCtrl,
    RightShift,
    RightAlt,
    RightGui,
    Mode = 0x0101,
    AudioNext,
    AudioPrev,
    AudioStop,
    AudioPlay,
    AudioMute,
    MediaSelect,
    Www,
    Mail,
    Calculator,
    Computer,
    ApplicationControlSearch,
    ApplicationControlHome,
    ApplicationControlBack,
    ApplicationControlForward,
    ApplicationControlStop,
    ApplicationControlRefresh,
    ApplicationControlBookmarks,
    BrightnessDown,
    BrightnessUp,
    DisplaySwitch,
    KeyboardIlluminationToggle,
    KeyboardIlluminationDown,
    KeyboardIlluminationUp,
    Eject,
    Sleep,
    App1,
    App2,
    AudioRewind,
    AudioFastForward,
    SoftLeft,
    SoftRight,
    Call,
    EndCall
}
