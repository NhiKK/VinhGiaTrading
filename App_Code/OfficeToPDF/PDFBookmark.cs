
using System.Collections;
using System.Collections.Generic;

namespace OfficeToPDF
{
    public class PDFBookmark
    {
        public int page { get; set; }
        public string title { get; set; }
        public List<PDFBookmark> children { get; set; }

        public static Hashtable createOptions()
        {
            Hashtable options = new Hashtable();
            options["hidden"] = false;
            options["markup"] = false;
            options["readonly"] = false;
            options["bookmarks"] = false;
            options["print"] = true;
            options["screen"] = false;
            options["pdfa"] = false;
            options["verbose"] = false;
            options["excludeprops"] = false;
            options["excludetags"] = false;
            options["noquit"] = false;
            options["merge"] = false;
            options["template"] = "";
            options["password"] = "";
            options["printer"] = "";
            options["fallback_printer"] = "";
            options["working_dir"] = "";
            options["has_working_dir"] = false;
            options["excel_show_formulas"] = false;
            options["excel_show_headings"] = false;
            options["excel_auto_macros"] = false;
            options["excel_template_macros"] = false;
            options["excel_active_sheet"] = false;
            options["excel_no_link_update"] = false;
            options["excel_no_recalculate"] = false;
            options["excel_max_rows"] = (int)0;
            options["excel_active_sheet_on_max_rows"] = false;
            options["excel_worksheet"] = (int)0;
            options["excel_delay"] = (int)0;
            options["word_field_quick_update"] = false;
            options["word_field_quick_update_safe"] = false;
            options["word_no_field_update"] = false;
            options["word_header_dist"] = (float)-1;
            options["word_footer_dist"] = (float)-1;
            options["word_max_pages"] = (int)0;
            options["word_ref_fonts"] = false;
            options["word_keep_history"] = false;
            options["word_no_repair"] = false;
            options["word_show_comments"] = false;
            options["word_show_revs_comments"] = false;
            options["word_show_format_changes"] = false;
            options["word_show_hidden"] = false;
            options["word_show_ink_annot"] = false;
            options["word_show_ins_del"] = false;
            options["word_markup_balloon"] = false;
            options["word_show_all_markup"] = false;
            options["word_fix_table_columns"] = false;
            options["original_filename"] = "";
            options["original_basename"] = "";
            options["powerpoint_output"] = "";
            options["pdf_page_mode"] = null;
            options["pdf_layout"] = null;
            options["pdf_merge"] = (int)MergeMode.None;
            options["pdf_clean_meta"] = (int)MetaClean.None;
            options["pdf_owner_pass"] = "";
            options["pdf_user_pass"] = "";
            options["pdf_restrict_annotation"] = false;
            options["pdf_restrict_extraction"] = false;
            options["pdf_restrict_assembly"] = false;
            options["pdf_restrict_forms"] = false;
            options["pdf_restrict_modify"] = false;
            options["pdf_restrict_print"] = false;
            options["pdf_restrict_annotation"] = false;
            options["pdf_restrict_accessibility_extraction"] = false;
            options["pdf_restrict_full_quality"] = false;
            return options;
        }
    }
}
