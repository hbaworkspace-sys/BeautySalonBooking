using BeautySalonBooking.Maui.Common.Helpers;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class OtpCodeEntry : ContentView
{
    private const int OtpLength = 6;

    private readonly Entry[] _entries;

    private readonly DigitConvertor _digitConvertor = new();

    private bool _isUpdating;
    private bool _isCompleted;

    public event EventHandler? OnCompleted;


    public OtpCodeEntry()
    {
        InitializeComponent();

        _entries =
        [
            txtDigit_1,
            txtDigit_2,
            txtDigit_3,
            txtDigit_4,
            txtDigit_5,
            txtDigit_6
        ];

        AttachEvents();
    }


    #region Initialization

    private void AttachEvents()
    {
        foreach (var entry in _entries)
        {
            entry.TextChanged += Entry_TextChanged;
            entry.Focused += Entry_Focused;
            entry.Completed += Entry_Completed;
        }
    }

    #endregion


    #region OtpCode

    public string OtpCode
    {
        get => (string)GetValue(OtpCodeProperty);
        set => SetValue(OtpCodeProperty, value);
    }


    public static readonly BindableProperty OtpCodeProperty =
        BindableProperty.Create(
            nameof(OtpCode),
            typeof(string),
            typeof(OtpCodeEntry),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnOtpCodeChanged);


    private static void OnOtpCodeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not OtpCodeEntry control)
            return;

        if (control._isUpdating)
            return;

        control.SetEntries(newValue?.ToString());
    }

    #endregion


    #region Text Changed

    private void Entry_TextChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        if (_isUpdating)
            return;

        if (sender is not Entry entry)
            return;

        int index = Array.IndexOf(_entries, entry);

        if (index < 0)
            return;


        string text = NormalizeDigits(e.NewTextValue);


        /*
         * Entry خالی شده است.
         *
         * این حالت معمولاً زمانی اتفاق می‌افتد که
         * کاربر Backspace زده است.
         */
        if (string.IsNullOrEmpty(text))
        {
            UpdateOtpCode();

            _isCompleted = false;

            ClearError();

            /*
             * اگر خانه قبلی وجود دارد،
             * Focus را روی خانه قبلی می‌بریم.
             */
            if (index > 0)
            {
                _entries[index - 1].Focus();

                SelectEntryText(
                    _entries[index - 1]);
            }

            return;
        }


        /*
         * اگر بیشتر از یک رقم باشد،
         * احتمالاً Paste انجام شده است.
         */
        if (text.Length > 1)
        {
            SetPastedCode(index, text);

            return;
        }


        /*
         * ورود یک رقم معمولی.
         */
        SetEntryText(
            entry,
            text);

        UpdateOtpCode();

        ClearError();


        /*
         * رفتن به خانه بعدی.
         */
        if (index < OtpLength - 1)
        {
            _entries[index + 1].Focus();

            return;
        }


        /*
         * خانه ششم پر شده است.
         */
        TryComplete();
    }

    #endregion


    #region Completed

    private void Entry_Completed(
        object? sender,
        EventArgs e)
    {
        if (sender is not Entry entry)
            return;

        int index =
            Array.IndexOf(
                _entries,
                entry);

        if (index < 0)
            return;


        /*
         * اگر خانه آخر نیست،
         * به خانه بعدی برو.
         */
        if (index < OtpLength - 1)
        {
            _entries[index + 1].Focus();

            return;
        }


        /*
         * اگر خانه ششم است،
         * OTP را بررسی کن.
         */
        TryComplete();
    }

    #endregion


    #region Paste

    private void SetPastedCode(
        int startIndex,
        string text)
    {
        _isUpdating = true;

        try
        {
            for (int i = 0; i < text.Length; i++)
            {
                int targetIndex =
                    startIndex + i;

                if (targetIndex >= OtpLength)
                    break;

                _entries[targetIndex].Text =
                    text[i].ToString();
            }
        }
        finally
        {
            _isUpdating = false;
        }


        UpdateOtpCode();

        ClearError();


        int lastFilledIndex =
            Math.Min(
                startIndex + text.Length - 1,
                OtpLength - 1);


        /*
         * اگر Paste باعث کامل شدن OTP شد.
         */
        if (lastFilledIndex == OtpLength - 1)
        {
            TryComplete();

            return;
        }


        /*
         * رفتن به اولین خانه خالی بعد از Paste.
         */
        _entries[lastFilledIndex + 1].Focus();
    }

    #endregion


    #region Focus

    private void Entry_Focused(
        object? sender,
        FocusEventArgs e)
    {
        if (sender is not Entry entry)
            return;

        SelectEntryText(entry);
    }


    public void FocusMe()
    {
        FocusFirstEmpty();
    }


    public void FocusFirstEmpty()
    {
        int index = GetFirstEmptyIndex();

        _entries[index].Focus();
    }


    public void Unfocus()
    {
        foreach (var entry in _entries)
            entry.Unfocus();
    }


    private int GetFirstEmptyIndex()
    {
        for (int i = 0; i < _entries.Length; i++)
        {
            if (string.IsNullOrEmpty(_entries[i].Text))
                return i;
        }

        return OtpLength - 1;
    }

    #endregion


    #region Text Selection

    private void SelectEntryText(Entry entry)
    {
#if ANDROID

        if (entry.Handler?.PlatformView
            is Android.Widget.EditText editText)
        {
            editText.Post(() =>
            {
                editText.SelectAll();
            });
        }

#elif WINDOWS

        if (entry.Handler?.PlatformView
            is Microsoft.UI.Xaml.Controls.TextBox textBox)
        {
            textBox.SelectAll();
        }

#endif
    }

    #endregion


    #region OtpCode Synchronization

    private void UpdateOtpCode()
    {
        if (_isUpdating)
            return;


        string code =
            string.Concat(
                _entries.Select(
                    x => x.Text ?? string.Empty));


        _isUpdating = true;

        try
        {
            SetValue(
                OtpCodeProperty,
                code);
        }
        finally
        {
            _isUpdating = false;
        }
    }


    private void SetEntries(string? value)
    {
        string code =
            NormalizeDigits(value);


        _isUpdating = true;

        try
        {
            for (int i = 0; i < OtpLength; i++)
            {
                _entries[i].Text =
                    i < code.Length
                        ? code[i].ToString()
                        : string.Empty;
            }
        }
        finally
        {
            _isUpdating = false;
        }
    }


    private void SetEntryText(
        Entry entry,
        string value)
    {
        if (entry.Text == value)
            return;


        _isUpdating = true;

        try
        {
            entry.Text = value;
        }
        finally
        {
            _isUpdating = false;
        }
    }

    #endregion


    #region Normalization

    private string NormalizeDigits(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;


        string text =
            _digitConvertor.ToEnglish(value);


        return new string(
            text
                .Where(char.IsDigit)
                .Where(x => x >= '0' && x <= '9')
                .Take(OtpLength)
                .ToArray());
    }

    #endregion


    #region Completed

    private void TryComplete()
    {
        string code = GetText();


        if (code.Length != OtpLength)
            return;


        if (_isCompleted)
            return;


        _isCompleted = true;

        Unfocus();


        OnCompleted?.Invoke(
            this,
            EventArgs.Empty);
    }

    #endregion


    #region Text Operations

    public string GetText()
    {
        return string.Concat(
            _entries.Select(
                x => x.Text ?? string.Empty));
    }


    public void SetText(string? value)
    {
        string code =
            NormalizeDigits(value);


        _isCompleted = false;


        SetEntries(code);


        _isUpdating = true;

        try
        {
            SetValue(
                OtpCodeProperty,
                code);
        }
        finally
        {
            _isUpdating = false;
        }


        if (code.Length == OtpLength)
            TryComplete();
    }


    public void Clear()
    {
        _isCompleted = false;

        _isUpdating = true;

        try
        {
            foreach (var entry in _entries)
                entry.Text = string.Empty;

            SetValue(
                OtpCodeProperty,
                string.Empty);
        }
        finally
        {
            _isUpdating = false;
        }


        ClearError();

        _entries[0].Focus();
    }

    #endregion


    #region Error

    public void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }


    public void ClearError()
    {
        ErrorLabel.Text = string.Empty;
        ErrorLabel.IsVisible = false;
    }

    #endregion
}