# Wion CLI - Architecture Documentation

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                         User                                │
│                    wion new <ProjectName>                   │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                      Program.cs                             │
│                   (Entry Point)                             │
│  - Initializes RootCommand                                  │
│  - Delegates to NewCommand                                  │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                    NewCommand.cs                            │
│  - Validates arguments                                      │
│  - Resolves template path                                   │
│  - Creates TemplateProcessor                                │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                 TemplateProcessor.cs                        │
│  (Orchestrates the entire process)                          │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ 1. CopyDirectoryAsync()                              │   │
│  │    - Copies template structure                       │   │
│  │    - Renames directories during copy                 │   │
│  └─────────────────────────────────────────────────────┘   │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ 2. ProcessFilesAsync()                               │   │
│  │    - Uses FileReplacer to update file contents      │   │
│  └─────────────────────────────────────────────────────┘   │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ 3. RenameDirectoriesAsync()                          │   │
│  │    - Uses DirectoryRenamer for remaining dirs       │   │
│  └─────────────────────────────────────────────────────┘   │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ 4. RenameFilesAsync()                                │   │
│  │    - Uses DirectoryRenamer for files                │   │
│  └─────────────────────────────────────────────────────┘   │
└───────────────────────────┬─────────────────────────────────┘
                            │
            ┌───────────────┼───────────────┐
            ▼               ▼               ▼
    ┌──────────────┐ ┌──────────────┐ ┌──────────────┐
    │ FileReplacer │ │ DirRenamer   │ │   Logger     │
    │              │ │              │ │              │
    │ - Replaces   │ │ - Renames    │ │ - Info       │
    │   content    │ │   dirs/files │ │ - Debug      │
    │ - Skips      │ │ - Handles    │ │ - Warning    │
    │   binaries   │ │   collisions │ │ - Error      │
    └──────────────┘ └──────────────┘ └──────────────┘
```

## Data Flow

```
Input: "Wion.Invoice"
   │
   ├─► Template Resolution
   │     └─► Find Wion.Template directory
   │
   ├─► Directory Copy
   │     ├─► Copy all files/directories
   │     └─► Rename during copy
   │
   ├─► File Content Processing
   │     ├─► Read each file (UTF-8)
   │     ├─► Replace "Wion.Template" → "Wion.Invoice"
   │     └─► Write back
   │
   ├─► Directory Renaming
   │     └─► Rename remaining directories
   │
   └─► File Renaming
         └─► Rename files containing template name

Output: Wion.Invoice/
         └─► Complete ABP project structure
```

## Key Design Decisions

### 1. Separation of Concerns

Each class has a single responsibility:
- **TemplateProcessor**: Orchestration
- **FileReplacer**: Content replacement
- **DirectoryRenamer**: File/directory renaming
- **Logger**: Logging

### 2. Processing Order

Files are processed in this specific order to avoid issues:
1. **Copy first**: Creates the base structure
2. **Content processing**: Updates what's inside files
3. **Directory renaming**: Renames containers (deepest first)
4. **File renaming**: Renames files last

### 3. Path Resolution

Multiple fallback paths ensure the template is found:
1. Published output location
2. Development location
3. Current directory
4. Relative to executable

### 4. Error Handling

- Clear error messages for common issues
- Verbose mode for debugging
- Non-destructive (won't overwrite existing directories)

## Extension Points

The CLI can be extended with:

1. **Additional Commands**: Add new commands following the `NewCommand` pattern
2. **Custom Replacements**: Extend `FileReplacer.GetReplacementStrings()`
3. **Template Sources**: Modify `ResolveTemplatePath()` for different sources
4. **File Filters**: Add more file types to skip/modify

## Performance Considerations

- **Async Operations**: All file I/O is async
- **Single Pass**: Files are read/written once
- **Binary Skipping**: Binary files are detected and skipped
- **Efficient Renaming**: Directories renamed deepest-first to minimize operations