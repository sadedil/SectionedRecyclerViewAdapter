#Sectioned Recycler View Adapter

**SectionedRecyclerViewAdapter** is offers a simple way to create **RecyclerView** adapters for Xamarin Android.

##Under the hood
This library has only one class inherits from Android's **RecyclerView.Adapter**.

In this class, these original methods are hidden:
```C#
public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)

public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
```

Instead of overriding original **RecyclerView.Adapter** methods, you have to override these new (much simpler) methods, similar to iOS's **TableView DataSource**:

```C#
///How many sections do you have?
virtual int NumbersOfSections();

///How many items do you have in this specified section?
abstract int RowsInSection(int section);

///How to inflate Item Views?
abstract RecyclerView.ViewHolder OnCreateItemViewHolder(ViewGroup parent);

///How to populate Item Views?
abstract void OnBindItemViewHolder(RecyclerView.ViewHolder holder, IndexPath indexPath);

///How to inflate Section Views? (optional)
virtual RecyclerView.ViewHolder OnCreateSectionViewHolder(ViewGroup parent);

///How to populate Section Views? (optional)
virtual void OnBindSectionViewHolder(RecyclerView.ViewHolder holder, int section);

///How to get item at this indexPath
abstract T GetItem(IndexPath indexPath);
```

##Why use this library? How it makes things easy?

Because, it's addresses item positions in custom `IndexPath` structure. It's easy to know, which element in which section?
```C#
class IndexPath
{
    int OriginalPosition { get; set; }
    int SectionIndex { get; set; }
    int? ItemIndex { get; set; }
}
```

##How can i use?

In the solution, you can find a demo project. It has two examples fulled with comments.
First one is basic example with minimum requirements, other one is slightly advanced.

I think this demo project enough for understanding the logic of this library.


##Any screenshot?

*You can find demo screen recordings below*

![Demo](help/demo.gif)