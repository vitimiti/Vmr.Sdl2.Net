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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

public enum KeyCode
{
    Unknown = 0,
    Return = '\r',
    Escape = '\x1B',
    Backspace = '\b',
    Tab = '\t',
    Space = ' ',
    Exclamation = '!',
    DoubleQuote = '"',
    Hash = '#',
    Percent = '%',
    Dollar = '$',
    Ampersand = '&',
    Quote = '\'',
    LeftParenthesis = '(',
    RightParenthesis = ')',
    Asterisk = '*',
    Plus = '+',
    Comma = ',',
    Minus = '-',
    Period = '.',
    Slash = '/',
    Number0 = '0',
    Number1 = '1',
    Number2 = '2',
    Number3 = '3',
    Number4 = '4',
    Number5 = '5',
    Number6 = '6',
    Number7 = '7',
    Number8 = '8',
    Number9 = '9',
    Colon = ':',
    Semicolon = ';',
    Less = '<',
    EqualsCode = '=',
    Greater = '>',
    Question = '?',
    At = '@',
    LeftBracket = '[',
    Backslash = '\\',
    RightBracket = ']',
    Caret = '^',
    Underscore = '_',
    BackQuote = '`',
    A = 'a',
    B = 'b',
    C = 'c',
    D = 'd',
    E = 'e',
    F = 'f',
    G = 'g',
    H = 'h',
    I = 'i',
    J = 'j',
    K = 'k',
    L = 'l',
    M = 'm',
    N = 'n',
    O = 'o',
    P = 'p',
    Q = 'q',
    R = 'r',
    S = 's',
    T = 't',
    U = 'u',
    V = 'v',
    W = 'w',
    X = 'x',
    Y = 'y',
    Z = 'z',
    CapsLock = ScanCode.CapsLock | ScanCodeData.Mask,
    F1 = ScanCode.F1 | ScanCodeData.Mask,
    F2 = ScanCode.F2 | ScanCodeData.Mask,
    F3 = ScanCode.F3 | ScanCodeData.Mask,
    F4 = ScanCode.F4 | ScanCodeData.Mask,
    F5 = ScanCode.F5 | ScanCodeData.Mask,
    F6 = ScanCode.F6 | ScanCodeData.Mask,
    F7 = ScanCode.F7 | ScanCodeData.Mask,
    F8 = ScanCode.F8 | ScanCodeData.Mask,
    F9 = ScanCode.F9 | ScanCodeData.Mask,
    F10 = ScanCode.F10 | ScanCodeData.Mask,
    F11 = ScanCode.F11 | ScanCodeData.Mask,
    F12 = ScanCode.F12 | ScanCodeData.Mask,
    PrintScreen = ScanCode.PrintScreen | ScanCodeData.Mask,
    ScrollLock = ScanCode.ScrollLock | ScanCodeData.Mask,
    Pause = ScanCode.Pause | ScanCodeData.Mask,
    Insert = ScanCode.Insert | ScanCodeData.Mask,
    Home = ScanCode.Home | ScanCodeData.Mask,
    PageUp = ScanCode.PageUp | ScanCodeData.Mask,
    Delete = '\x7F',
    End = ScanCode.End | ScanCodeData.Mask,
    PageDown = ScanCode.PageDown | ScanCodeData.Mask,
    Right = ScanCode.Right | ScanCodeData.Mask,
    Left = ScanCode.Left | ScanCodeData.Mask,
    Down = ScanCode.Down | ScanCodeData.Mask,
    Up = ScanCode.Up | ScanCodeData.Mask,
    NumLockClear = ScanCode.NumLockClear | ScanCodeData.Mask,
    KeypadDivide = ScanCode.KeypadDivide | ScanCodeData.Mask,
    KeypadMultiply = ScanCode.KeypadMultiply | ScanCodeData.Mask,
    KeypadMinus = ScanCode.KeypadMinus | ScanCodeData.Mask,
    KeypadPlus = ScanCode.KeypadPlus | ScanCodeData.Mask,
    KeypadEnter = ScanCode.KeypadEnter | ScanCodeData.Mask,
    KeypadNumber1 = ScanCode.KeypadNumber1 | ScanCodeData.Mask,
    KeypadNumber2 = ScanCode.KeypadNumber2 | ScanCodeData.Mask,
    KeypadNumber3 = ScanCode.KeypadNumber3 | ScanCodeData.Mask,
    KeypadNumber4 = ScanCode.KeypadNumber4 | ScanCodeData.Mask,
    KeypadNumber5 = ScanCode.KeypadNumber5 | ScanCodeData.Mask,
    KeypadNumber6 = ScanCode.KeypadNumber6 | ScanCodeData.Mask,
    KeypadNumber7 = ScanCode.KeypadNumber7 | ScanCodeData.Mask,
    KeypadNumber8 = ScanCode.KeypadNumber8 | ScanCodeData.Mask,
    KeypadNumber9 = ScanCode.KeypadNumber9 | ScanCodeData.Mask,
    KeypadNumber0 = ScanCode.KeypadNumber0 | ScanCodeData.Mask,
    KeypadPeriod = ScanCode.KeypadPeriod | ScanCodeData.Mask,
    Application = ScanCode.Application | ScanCodeData.Mask,
    Power = ScanCode.Power | ScanCodeData.Mask,
    KeypadEquals = ScanCode.KeypadEquals | ScanCodeData.Mask,
    F13 = ScanCode.F13 | ScanCodeData.Mask,
    F14 = ScanCode.F14 | ScanCodeData.Mask,
    F15 = ScanCode.F15 | ScanCodeData.Mask,
    F16 = ScanCode.F16 | ScanCodeData.Mask,
    F17 = ScanCode.F17 | ScanCodeData.Mask,
    F18 = ScanCode.F18 | ScanCodeData.Mask,
    F19 = ScanCode.F19 | ScanCodeData.Mask,
    F20 = ScanCode.F20 | ScanCodeData.Mask,
    F21 = ScanCode.F21 | ScanCodeData.Mask,
    F22 = ScanCode.F22 | ScanCodeData.Mask,
    F23 = ScanCode.F23 | ScanCodeData.Mask,
    F24 = ScanCode.F24 | ScanCodeData.Mask,
    Execute = ScanCode.Execute | ScanCodeData.Mask,
    Help = ScanCode.Help | ScanCodeData.Mask,
    Menu = ScanCode.Menu | ScanCodeData.Mask,
    Select = ScanCode.Select | ScanCodeData.Mask,
    Stop = ScanCode.Stop | ScanCodeData.Mask,
    Again = ScanCode.Again | ScanCodeData.Mask,
    Undo = ScanCode.Undo | ScanCodeData.Mask,
    Cut = ScanCode.Cut | ScanCodeData.Mask,
    Copy = ScanCode.Copy | ScanCodeData.Mask,
    Paste = ScanCode.Paste | ScanCodeData.Mask,
    Find = ScanCode.Find | ScanCodeData.Mask,
    Mute = ScanCode.Mute | ScanCodeData.Mask,
    VolumeUp = ScanCode.VolumeUp | ScanCodeData.Mask,
    VolumeDown = ScanCode.VolumeDown | ScanCodeData.Mask,
    KeypadComma = ScanCode.KeypadComma | ScanCodeData.Mask,
    KeypadEqualsAs400 = ScanCode.KeypadEqualsAs400 | ScanCodeData.Mask,
    AltErase = ScanCode.AltErase | ScanCodeData.Mask,
    SysReq = ScanCode.SysReq | ScanCodeData.Mask,
    Cancel = ScanCode.Cancel | ScanCodeData.Mask,
    Clear = ScanCode.Clear | ScanCodeData.Mask,
    Prior = ScanCode.Prior | ScanCodeData.Mask,
    Return2 = ScanCode.Return2 | ScanCodeData.Mask,
    Separator = ScanCode.Separator | ScanCodeData.Mask,
    Out = ScanCode.Out | ScanCodeData.Mask,
    Oper = ScanCode.Oper | ScanCodeData.Mask,
    ClearAgain = ScanCode.ClearAgain | ScanCodeData.Mask,
    CrSel = ScanCode.CrSel | ScanCodeData.Mask,
    ExSel = ScanCode.ExSel | ScanCodeData.Mask,
    KeypadNumber00 = ScanCode.KeypadNumber00 | ScanCodeData.Mask,
    KeypadNumber000 = ScanCode.KeypadNumber000 | ScanCodeData.Mask,
    ThousandsSeparator = ScanCode.ThousandsSeparator | ScanCodeData.Mask,
    DecimalSeparator = ScanCode.DecimalSeparator | ScanCodeData.Mask,
    CurrencyUnit = ScanCode.CurrencyUnit | ScanCodeData.Mask,
    CurrencySubunit = ScanCode.CurrencySubunit | ScanCodeData.Mask,
    KeypadLeftParenthesis = ScanCode.KeypadLeftParenthesis | ScanCodeData.Mask,
    KeypadRightParenthesis = ScanCode.KeypadRightParenthesis | ScanCodeData.Mask,
    KeypadLeftBrace = ScanCode.KeypadLeftBrace | ScanCodeData.Mask,
    KeypadRightBrace = ScanCode.KeypadRightBrace | ScanCodeData.Mask,
    KeypadTab = ScanCode.KeypadTab | ScanCodeData.Mask,
    KeypadBackspace = ScanCode.KeypadBackspace | ScanCodeData.Mask,
    KeypadA = ScanCode.KeypadA | ScanCodeData.Mask,
    KeypadB = ScanCode.KeypadB | ScanCodeData.Mask,
    KeypadC = ScanCode.KeypadC | ScanCodeData.Mask,
    KeypadD = ScanCode.KeypadD | ScanCodeData.Mask,
    KeypadE = ScanCode.KeypadE | ScanCodeData.Mask,
    KeypadF = ScanCode.KeypadF | ScanCodeData.Mask,
    KeypadXor = ScanCode.KeypadXor | ScanCodeData.Mask,
    KeypadPower = ScanCode.KeypadPower | ScanCodeData.Mask,
    KeypadPercent = ScanCode.KeypadPercent | ScanCodeData.Mask,
    KeypadLess = ScanCode.KeypadLess | ScanCodeData.Mask,
    KeypadGreater = ScanCode.KeypadGreater | ScanCodeData.Mask,
    KeypadAmpersand = ScanCode.KeypadAmpersand | ScanCodeData.Mask,
    KeypadDoubleAmpersand = ScanCode.KeypadDoubleAmpersand | ScanCodeData.Mask,
    KeypadVerticalBar = ScanCode.KeypadVerticalBar | ScanCodeData.Mask,
    KeypadDoubleVerticalBar = ScanCode.KeypadDoubleVerticalBar | ScanCodeData.Mask,
    KeypadColon = ScanCode.KeypadColon | ScanCodeData.Mask,
    KeypadHash = ScanCode.KeypadHash | ScanCodeData.Mask,
    KeypadSpace = ScanCode.KeypadSpace | ScanCodeData.Mask,
    KeypadAt = ScanCode.KeypadAt | ScanCodeData.Mask,
    KeypadExclamation = ScanCode.KeypadExclamation | ScanCodeData.Mask,
    KeypadMemoryStore = ScanCode.KeypadMemoryStore | ScanCodeData.Mask,
    KeypadMemoryRecall = ScanCode.KeypadMemoryRecall | ScanCodeData.Mask,
    KeypadMemoryClear = ScanCode.KeypadMemoryClear | ScanCodeData.Mask,
    KeypadMemoryAdd = ScanCode.KeypadMemoryAdd | ScanCodeData.Mask,
    KeypadMemorySubtract = ScanCode.KeypadMemorySubtract | ScanCodeData.Mask,
    KeypadMemoryMultiply = ScanCode.KeypadMemoryMultiply | ScanCodeData.Mask,
    KeypadMemoryDivide = ScanCode.KeypadMemoryDivide | ScanCodeData.Mask,
    KeypadPlusMinus = ScanCode.KeypadPlusMinus | ScanCodeData.Mask,
    KeypadClear = ScanCode.KeypadClear | ScanCodeData.Mask,
    KeypadClearEntry = ScanCode.KeypadClearEntry | ScanCodeData.Mask,
    KeypadBinary = ScanCode.KeypadBinary | ScanCodeData.Mask,
    KeypadOctal = ScanCode.KeypadOctal | ScanCodeData.Mask,
    KeypadDecimal = ScanCode.KeypadDecimal | ScanCodeData.Mask,
    KeypadHexadecimal = ScanCode.KeypadHexadecimal | ScanCodeData.Mask,
    LeftCtrl = ScanCode.LeftCtrl | ScanCodeData.Mask,
    LeftShift = ScanCode.LeftShift | ScanCodeData.Mask,
    LeftAlt = ScanCode.LeftAlt | ScanCodeData.Mask,
    LeftGui = ScanCode.LeftGui | ScanCodeData.Mask,
    RightCtrl = ScanCode.RightCtrl | ScanCodeData.Mask,
    RightShift = ScanCode.RightShift | ScanCodeData.Mask,
    RightAlt = ScanCode.RightAlt | ScanCodeData.Mask,
    RightGui = ScanCode.RightGui | ScanCodeData.Mask,
    Mode = ScanCode.Mode | ScanCodeData.Mask,
    AudioNext = ScanCode.AudioNext | ScanCodeData.Mask,
    AudioPrev = ScanCode.AudioPrev | ScanCodeData.Mask,
    AudioStop = ScanCode.AudioStop | ScanCodeData.Mask,
    AudioPlay = ScanCode.AudioPlay | ScanCodeData.Mask,
    AudioMute = ScanCode.AudioMute | ScanCodeData.Mask,
    MediaSelect = ScanCode.MediaSelect | ScanCodeData.Mask,
    Www = ScanCode.Www | ScanCodeData.Mask,
    Mail = ScanCode.Mail | ScanCodeData.Mask,
    Calculator = ScanCode.Calculator | ScanCodeData.Mask,
    Computer = ScanCode.Computer | ScanCodeData.Mask,
    ApplicationControlSearch = ScanCode.ApplicationControlSearch | ScanCodeData.Mask,
    ApplicationControlHome = ScanCode.ApplicationControlHome | ScanCodeData.Mask,
    ApplicationControlBack = ScanCode.ApplicationControlBack | ScanCodeData.Mask,
    ApplicationControlForward = ScanCode.ApplicationControlForward | ScanCodeData.Mask,
    ApplicationControlStop = ScanCode.ApplicationControlStop | ScanCodeData.Mask,
    ApplicationControlRefresh = ScanCode.ApplicationControlRefresh | ScanCodeData.Mask,
    ApplicationControlBookmarks = ScanCode.ApplicationControlBookmarks | ScanCodeData.Mask,
    BrightnessDown = ScanCode.BrightnessDown | ScanCodeData.Mask,
    BrightnessUp = ScanCode.BrightnessUp | ScanCodeData.Mask,
    DisplaySwitch = ScanCode.DisplaySwitch | ScanCodeData.Mask,
    KeyboardIlluminationToggle = ScanCode.KeyboardIlluminationToggle | ScanCodeData.Mask,
    KeyboardIlluminationDown = ScanCode.KeyboardIlluminationDown | ScanCodeData.Mask,
    KeyboardIlluminationUp = ScanCode.KeyboardIlluminationUp | ScanCodeData.Mask,
    Eject = ScanCode.Eject | ScanCodeData.Mask,
    Sleep = ScanCode.Sleep | ScanCodeData.Mask,
    App1 = ScanCode.App1 | ScanCodeData.Mask,
    App2 = ScanCode.App2 | ScanCodeData.Mask,
    AudioRewind = ScanCode.AudioRewind | ScanCodeData.Mask,
    AudioFastForward = ScanCode.AudioFastForward | ScanCodeData.Mask,
    SoftLeft = ScanCode.SoftLeft | ScanCodeData.Mask,
    SoftRight = ScanCode.SoftRight | ScanCodeData.Mask,
    Call = ScanCode.Call | ScanCodeData.Mask,
    EndCall = ScanCode.EndCall | ScanCodeData.Mask
}
