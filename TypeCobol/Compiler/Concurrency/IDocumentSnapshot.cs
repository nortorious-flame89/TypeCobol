﻿using System;
using System.Collections.Generic;

namespace TypeCobol.Compiler.Concurrency
{
    /// <summary>
    /// Represents an immutable snapshot of a specific version of a document.
    /// </summary>
    /// <remarks>
    /// Documents snapshots enable one thread to consume information concurrently produced by another thread.
    /// </remarks>
    public interface IDocumentSnapshot<DocumentChangeType>
    {
        /// <summary>
        /// Document version identifier for this snapshot
        /// </summary>
        IDocumentVersion<DocumentChangeType> Version { get; }
    }
}