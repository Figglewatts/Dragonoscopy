using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace TextFileContentPipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".json", DisplayName = "JSON file importer", DefaultProcessor = "JSONContentProcessor")]
    public class JSONContentImporter : ContentImporter<String>
    {
        public override String Import(string filename, ContentImporterContext context)
        {
            using (var file = File.OpenText(filename))
            {
                return file.ReadToEnd();
            }
        }

    }

}
