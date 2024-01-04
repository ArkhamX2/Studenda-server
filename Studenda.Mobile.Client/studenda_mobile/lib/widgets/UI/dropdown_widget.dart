import 'package:flutter/material.dart';

class StudendaDropdown<T> extends StatefulWidget {
  final List<T?> items;
  final T? model;
  final Function(T?) callback;

  const StudendaDropdown(
      {super.key, required this.items,required this.model, required this.callback,});

  @override
  State<StudendaDropdown<T>> createState() => StudendaDropdownState<T>();
}

class StudendaDropdownState<T> extends State<StudendaDropdown<T>> {
  T? item;

  @override
  void initState() {
    super.initState();
    item = widget.model;
  }

  @override
  Widget build(BuildContext context) {
    return DropdownButton<T>(
      value: item,
      items: widget.items.map((T? value) {
        return DropdownMenuItem<T>(
          value: value,
          child: Text(value.toString()),
        );
      }).toList(),
      onChanged: (value) {
        widget.callback(value);
        setState(() {
          item = value;
        });
      },
    );
  }
}
