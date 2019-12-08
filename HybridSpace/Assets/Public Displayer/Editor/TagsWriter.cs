using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;

namespace PublicDisplayer
{
    [InitializeOnLoad]
    public class TagsWriter : Writer
    {
        private const string fileName = "Tags";
        private static int tagsLength = 7;

        static TagsWriter()
        {
            EditorApplication.update += CheckTagsChange;
        }

        private static void CheckTagsChange()
        {
            string[] tags = InternalEditorUtility.tags;
            if(tagsLength != tags.Length)
                WriteTagsScript(tags);
        }

        public static void ChangeTags()
        {
            string[] tags = InternalEditorUtility.tags;
            WriteTagsScript(tags);
        }

        private static void WriteTagsScript(string[] tags)
        {
            tagsLength = tags.Length;

            string[] body = tags.ToIdentifiers(fileName).
                Select((id, i) => ToVariable(id, tags[i])).
                ToArray();

            OverwriteOrAddFile(fileName, body, typeof(TagsWriter));
        }
    }
}
