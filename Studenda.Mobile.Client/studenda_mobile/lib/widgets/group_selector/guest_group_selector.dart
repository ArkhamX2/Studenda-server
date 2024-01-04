import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/course.dart';
import 'package:studenda_mobile/model/department.dart';
import 'package:studenda_mobile/model/group.dart';

final List<Department> departments = [
  Department("ФИТ"),
  Department("ХТФ"),
  Department("ФУСК"),
  Department("МШФ"),
];
final List<Course> courses = [
  Course(0,"1 курс", departments[0]),
  Course(1,"2 курс", departments[0]),
  Course(2,"3 курс", departments[0]),
  Course(3,"4 курс", departments[0]),
  Course(4,"1 курс", departments[1]),
  Course(5,"2 курс", departments[1]),
  Course(6,"3 курс", departments[1]),
  Course(7,"4 курс", departments[1]),
  Course(8,"1 курс", departments[2]),
  Course(9,"2 курс", departments[2]),
  Course(10,"3 курс", departments[2]),
  Course(11,"4 курс", departments[2]),
  Course(12,"1 курс", departments[3]),
  Course(13,"2 курс", departments[3]),
  Course(14,"3 курс", departments[3]),
  Course(15,"4 курс", departments[3]),
];
final List<Group> groups = [
  Group("PIN2107", departments[0], courses[2]),
  Group("PIN2106", departments[0], courses[1]),
  Group("PIN2206", departments[0], courses[2]),
  Group("PIN2306", departments[0], courses[0]),
];

class GuestGroupSelectorWidget extends StatefulWidget {
  const GuestGroupSelectorWidget({super.key});

  @override
  State<GuestGroupSelectorWidget> createState() =>
      _GuestGroupSelectorWidgetState();
}

class _GuestGroupSelectorWidgetState extends State<GuestGroupSelectorWidget> {
  String departmentsDropdownValue = departments.first.name;
  String coursesDropdownValue = courses.first.id.toString();
  String groupsDropdownValue = groups.first.name;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          onPressed: () => {},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Выбор группы',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
      ),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 17),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              DropdownButton(
                value: departmentsDropdownValue,
                items: departments.map((Department value) {
                  return DropdownMenuItem<String>(
                    value: value.name,
                    child: Text(value.name),
                  );
                }).toList(),
                onChanged: (String? value) {
                  setState(() {
                    departmentsDropdownValue = value!;
                  });
                },
              ),
              DropdownButton(
                value: coursesDropdownValue,
                items: courses.map((Course value) {
                  return DropdownMenuItem<String>(
                    value: value.id.toString(),
                    child: Text(value.name),
                  );
                }).toList(),
                onChanged: (String? value) {
                  setState(() {
                    coursesDropdownValue = value!;
                  });
                },
              ),
              DropdownButton(
                value: groupsDropdownValue,
                items: groups.map<DropdownMenuItem<String>>((Group value) {
                  return DropdownMenuItem<String>(
                    value: value.name,
                    child: Text(value.name),
                  );
                }).toList(),
                onChanged: (String? value) {
                  setState(() {
                    groupsDropdownValue = value!;
                  });
                },
              ),
              ElevatedButton(
                onPressed: () {},
                style: ButtonStyle(
                  minimumSize: const MaterialStatePropertyAll(Size(300, 50)),
                  shape: MaterialStatePropertyAll(
                    RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(9),
                    ),
                  ),
                  backgroundColor: const MaterialStatePropertyAll(
                    Color.fromARGB(255, 231, 225, 255),
                  ),
                ),
                child: const Text(
                  "Подтвердить",
                  style: TextStyle(
                    color: Color.fromARGB(255, 101, 59, 159),
                    fontSize: 23,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
