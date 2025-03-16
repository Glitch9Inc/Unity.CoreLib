using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// Custom Editor Icons
    /// </summary>
    public static partial class EditorIcons
    {
        // Arrow
        public static Texture MoveUp => Get("16_up.png");
        public static Texture MoveDown => Get("16_down.png");

        // Media
        //public static Texture PlayButton => Get("ic_play_arrow_black_36dp.png");
        //public static Texture PauseButton => Get("ic_pause_black_36dp.png");
        //public static Texture StopButton => Get("ic_stop_black_36dp.png");
        public static Texture NextButton => Get("ic_skip_next_black_36dp.png");
        public static Texture PreviousButton => Get("ic_skip_previous_black_36dp.png");
        public static Texture FastForwardButton => Get("ic_fast_forward_black_36dp.png");
        public static Texture RewindButton => Get("ic_fast_rewind_black_36dp.png");
        public static Texture VolumeButton => Get("ic_volumebar.png");
        public static Texture SeekBar => Get("ic_seekbar.png");
        public static Texture ShuffleButton => Get("baseline_shuffle_black_36.png");
        public static Texture RepeatButton => Get("baseline_repeat_black_36.png");
        public static Texture RepeatOneButton => Get("baseline_repeat_one_black_36.png");

        // dot
        public static Texture Dot => Get("16_dot.png");


        // File
        public static Texture AddFile => Get("add-file.png");
        public static Texture DeleteFile => Get("delete-file.png");
        public static Texture CheckFile => Get("check-file.png");
        public static Texture EditFile => Get("edit-file.png");
        public static Texture ExportCSV => Get("export-csv.png");
        public static Texture ImportCSV => Get("import-csv.png");
        public static Texture SplitFiles => Get("split-files.png");
        public static Texture MergeFiles => Get("merge-files.png");
        public static Texture UpdateFile => Get("update-file.png");
        public static Texture FileDelete => Get("file-delete.png");
        public static Texture FileSettings => Get("file-settings.png");
        public static Texture OpenFolder => Get("ic_open_folder.png");
        public static Texture Edit => Get("edit-file.png");

        // Editing
        public static Texture Add => Get("T_Add.png");
        public static Texture Clear => Get("T_Clean.png");
        public static Texture Delete => Get("T_Delete.png");
        public static Texture Done => Get("T_Done.png");
        public static Texture Export => Get("T_Export.png");
        public static Texture Import => Get("T_Import.png");
        public static Texture Language => Get("T_Language.png");
        public static Texture Question => Get("T_Question.png");
        public static Texture Quotes => Get("T_Quotes.png");
        public static Texture Return => Get("T_Return.png");
        public static Texture Save => Get("T_Save.png");
        public static Texture Start => Get("T_Start.png");
        public static Texture Translate => Get("T_Translate.png");
        public static Texture FindAndReplace => Get("ic_find_and_replace.png");
        public static Texture Replace => Get("ic_replace.png");
        public static Texture Group => Get("ic_group.png");


        // Extra
        public static Texture Information => Get("ic_information.png");
        public static Texture Phone => Get("ic_phone.psd");
        public static Texture Enter => Get("ic_enter.png");
        public static Texture List => Get("ic_list.png");
        public static Texture AI => Get("ic_ai.png");
        public static Texture Trash => Get("T_Delete.png");
        public static Texture NoImageHighRes => Get("no-image-high-res.png");
        public static Texture History => Get("ic_history.png");

        // Custom Editor Status (Added on 2024.06.23)
        public static Texture StatusCheck => Get("ic_status_check.png");
        public static Texture StatusError => Get("ic_status_error.png");
        public static Texture StatusWarning => Get("ic_status_warning.png");

        public static Texture LightModeOn => Get("ic_lightmode_on.png");
        public static Texture LightModeOff => Get("ic_lightmode_off.png");
        public static Texture DarkModeOn => Get("ic_darkmode_on.png");
        public static Texture DarkModeOff => Get("ic_darkmode_off.png");
        public static Texture LightModeSwitch => Get("ic_lightmode_switch.png");
        public static Texture ProVersion => Get("ic_pro_version.png");
    }
}
