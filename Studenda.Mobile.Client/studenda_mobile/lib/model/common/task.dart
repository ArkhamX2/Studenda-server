import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';

class Task {
  final int id;
  final String name;
  final SubjectEntity subject;
  final int mark;
  Task(this.id, this.name, this.subject, this.mark);

  @override
  String toString() {
    return name;
  }
}
