

import 'package:studenda_mobile/model/common/course.dart';
import 'package:studenda_mobile/model/common/department.dart';

class Group {
  final String name;
  final Department department;
  final Course course;
  Group(this.name, this.department, this.course);

  @override
  String toString() {
    // TODO: implement toString
    return name;
  }
}
