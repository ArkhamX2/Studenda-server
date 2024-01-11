import 'package:studenda_mobile/model/common/department.dart';

class Course {
  final int id;
  final String name;
  final Department department;
  Course(this.id, this.name, this.department);

  @override
  String toString() {
    // TODO: implement toString
    return name;
  }
}
