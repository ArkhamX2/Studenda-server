class Mark {
  final int id;
  final String date;
  final String value;
  Mark(this.id, this.date, this.value);

  @override
  String toString() {
    return "$date $value";
  }
}
