import 'package:studenda_mobile/model/course.dart';
import 'package:studenda_mobile/model/department.dart';

class Group {
  final String name;
  final Department department;
  final Course course;
  Group(this.name, this.department, this.course);
}
