import 'package:studenda_mobile/model/schedule/subject.dart';

class Task {
  final int id;
  final String name;
  final Subject subject;
  final int mark;
  Task(this.id, this.name, this.subject, this.mark);

  @override
  String toString() {
    return name;
  }
}
