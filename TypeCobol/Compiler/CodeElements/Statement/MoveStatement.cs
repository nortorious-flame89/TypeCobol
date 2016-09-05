﻿using JetBrains.Annotations;

namespace TypeCobol.Compiler.CodeElements {

	using System.Collections.Generic;
	using TypeCobol.Compiler.CodeElements.Expressions;

/// <summary>p369: The MOVE statement transfers data from one area of storage to one or more other areas.</summary>
public abstract class MoveStatement : StatementElement, Sending,VariableWriter,FunctionCaller {
	public MoveStatement(StatementType statementType) : base(CodeElementType.MoveStatement, statementType) { }
// [TYPECOBOL]
	public SyntaxProperty<bool> Unsafe { get; set; }
	public bool IsUnsafe { get { return Unsafe != null && Unsafe.Value; } }
// [/TYPECOBOL]
	public virtual IList<QualifiedName> Variables { get { return new List<QualifiedName>(); } }
	public virtual IList<QualifiedName> SendingItems { get { return new List<QualifiedName>(); } }
	public virtual IDictionary<QualifiedName,object> VariablesWritten { get { return new Dictionary<QualifiedName,object>(); } }
	public virtual IList<Functions.FunctionCall> FunctionCalls { get { return new List<Functions.FunctionCall>(); } }
}

/// <summary>
///  p369: Format 1: MOVE statement
///
/// When format 1 (no CORRESPONDING) is specified:
/// * All identifiers can reference alphanumeric group items, national group items, or
/// elementary items.
/// * When one of identifier-1 or identifier-2 references a national group item and the
/// other operand references an alphanumeric group item, the national group is
/// processed as a group item; in all other cases, the national group item is
/// processed as an elementary data item of category national.
/// * The data in the sending area is moved into the data item referenced by each
/// identifier-2 in the order in which the identifier-2 data items are specified in the
/// MOVE statement. See “Elementary moves” on page 370 and “Group moves” on page 374 below.
/// </summary>
public class MoveSimpleStatement : MoveStatement {
	public MoveSimpleStatement(Variable sendingVariable, ReceivingStorageArea[] receivingStorageAreas, BooleanValue sendingBoolean)
			: base(StatementType.MoveSimpleStatement) {
		SendingVariable = sendingVariable;
		SendingBoolean = sendingBoolean;
		ReceivingStorageAreas = receivingStorageAreas;
	}

	/// <summary>The sending area.</summary>
	public Variable SendingVariable { get; private set; }
// [TYPECOBOL]
	public BooleanValue SendingBoolean { get; private set; }
// [/TYPECOBOL]
	/// <summary>The receiving areas. Must not reference an intrinsic function.</summary>
	public ReceivingStorageArea[] ReceivingStorageAreas { get; private set; }


	private IDictionary<QualifiedName, object> _variablesWritten;
	private IList<QualifiedName> _sendingItems = null;
	private List<QualifiedName> _variables = null;
	private List<Functions.FunctionCall> _functions = null;


	public override IList<QualifiedName> Variables {
		[NotNull]
		get {
			if (_variables == null) {
				_variables = new List<QualifiedName>();
				var sending = SendingItem as QualifiedName;
				if (sending != null) _variables.Add(sending);
				_variables.AddRange(VariablesWritten.Keys);
			}
			return _variables;
		}
	}

	public override IList<QualifiedName> SendingItems {
		[NotNull]
		get {
			if (_sendingItems == null) {
				_sendingItems = new List<QualifiedName>();
				if (SendingVariable != null && SendingVariable.Name != null) _sendingItems.Add(SendingVariable.QualifiedName);
			}
			return _sendingItems;
		}
	}

	private object SendingItem {
		[CanBeNull]
		get {
			if (SendingVariable != null) return SendingVariable.QualifiedName;
			else if (SendingBoolean != null) return SendingBoolean.Value;
			else return null;
		}
	}

	public override IDictionary<QualifiedName,object> VariablesWritten {
		[NotNull]
		get {
			if (_variablesWritten != null) return _variablesWritten;

			_variablesWritten = new Dictionary<QualifiedName,object>();
			if (ReceivingStorageAreas == null) return _variablesWritten;

			foreach(var item in ReceivingStorageAreas) {
				var name = ((Named)item.StorageArea).QualifiedName;
				if (_variablesWritten.ContainsKey(name))
					if (item.StorageArea is Subscripted) continue; // same variable with (presumably) different subscript
					else throw new System.ArgumentException(name+" already written, but not subscripted?");
				else _variablesWritten.Add(name, SendingItem);
			}
			return _variablesWritten;
		}
	}

	public override IList<Functions.FunctionCall> FunctionCalls {
		[NotNull]
		get {
			if (_functions == null) {
				_functions = new List<Functions.FunctionCall>();
				var sending = SendingVariable.StorageArea as IntrinsicFunctionCallResult;
				if (sending != null) _functions.Add(new Functions.FunctionCall(sending));
			}
			return _functions;
		}
	}

}

/// <summary>
/// p369: Format 2: MOVE statement with CORRESPONDING phrase
///
/// When format 2 (with CORRESPONDING) is specified:
/// * Both identifiers must be group items.
/// * A national group item is processed as a group item (and not as an elementary
/// data item of category national).
/// * Selected items in identifier-1 are moved to identifier-2 according to the rules for
/// the “CORRESPONDING phrase” on page 281. The results are the same as if
/// each pair of CORRESPONDING identifiers were referenced in a separate MOVE
/// statement.
/// </summary>
public class MoveCorrespondingStatement : MoveStatement {
	public MoveCorrespondingStatement() : base(StatementType.MoveCorrespondingStatement) { }

	/// <summary>
	/// identifier-1
	/// The sending group item.
	/// </summary>
	public DataOrConditionStorageArea FromGroupItem { get; set; }

	/// <summary>
	/// identifier-2
	/// The receiving group item.
	/// </summary>
	public DataOrConditionStorageArea ToGroupItem { get; set; }
}



}
