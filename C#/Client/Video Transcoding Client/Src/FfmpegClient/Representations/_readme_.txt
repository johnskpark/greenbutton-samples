This folder contains .NET classes that serialise to the REST API resource representations.
This allows you to use methods such as PostAsXmlAsync<T>, PostAsJsonAsync<T> and
ReadAsAsync<T> rather than manually constructing XML or JSON message bodies.

The classes can be used for both XML and JSON media types.  The DataContract, DataMember
and EnumMember attributes are required by the XML serialiser.  If you are using JSON,
you do NOT need to provide these attributes, which improves the readability of the code.
