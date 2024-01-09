import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';
import 'package:studenda_mobile/feature/schedule/presentation/widgets/position_values.dart';
import 'package:studenda_mobile/resources/colors.dart';

//По нажатию на элемент выводить имя преподавателя ниже
class ScheduleItemWidget extends StatefulWidget {
  final SubjectEntity subject;
  const ScheduleItemWidget({required this.subject});

  @override
  State<ScheduleItemWidget> createState() => _ScheduleItemWidgetState();
}

class _ScheduleItemWidgetState extends State<ScheduleItemWidget> {
  bool isVisible = false;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        GestureDetector(
          onTap: () => setState(() {
            isVisible = !isVisible;
          }),
          child: _MainSubjectInfoRowWidget(
            widget: widget,
            isTeacherVisible: isVisible,
          ),
        ),
        AnimatedSize(
          duration: const Duration(milliseconds: 200),
          child: isVisible
              ? _TeacherSubjectInfoRowWidget(
                  widget: widget,
                )
              : Container(),
        ),
      ],
    );
  }
}

class _TeacherSubjectInfoRowWidget extends StatelessWidget {
  const _TeacherSubjectInfoRowWidget({
    required this.widget,
  });

  final ScheduleItemWidget widget;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.fromLTRB(8, 0, 8, 8),
      child: Row(
        children: [
          const SizedBox(
            width: 62,
          ),
          Expanded(
            child: Container(
              height: 46,
              decoration: BoxDecoration(
                borderRadius: const BorderRadius.only(
                  bottomLeft: Radius.circular(5),
                  bottomRight: Radius.circular(5),
                ),
                border: Border.all(
                  color: const Color.fromARGB(60, 0, 0, 0),
                ),
              ),
              child: Row(
                children: [
                  const SizedBox(
                    width: 15,
                  ),
                  Expanded(
                    child: Text(
                      widget.subject.teacher,
                      style: const TextStyle(
                        color: mainForegroundColor,
                        fontSize: 16,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class _MainSubjectInfoRowWidget extends StatelessWidget {
  const _MainSubjectInfoRowWidget({
    required this.widget,
    required this.isTeacherVisible,
  });

  final ScheduleItemWidget widget;
  final bool isTeacherVisible;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: !isTeacherVisible
          ? const EdgeInsets.all(8.0)
          : const EdgeInsets.fromLTRB(8, 8, 8, 0),
      child: IntrinsicHeight(
        child: Row(
          children: [
            SizedBox(
              width: 46,
              height: 46,
              child: Text(
                dayPositionValues[widget.subject.dayPosition],
                textAlign: TextAlign.right,
                style: const TextStyle(
                  color: mainForegroundColor,
                  fontSize: 16,
                ),
              ),
            ),
            const VerticalDivider(
              thickness: 1,
              indent: 5,
              endIndent: 5,
              color: Colors.grey,
            ),
            Expanded(
              child: Container(
                decoration: BoxDecoration(
                  borderRadius: !isTeacherVisible
                      ? BorderRadius.circular(5)
                      : const BorderRadius.only(
                          topLeft: Radius.circular(5),
                          topRight: Radius.circular(5),
                        ),
                  border: Border.all(
                    color: const Color.fromARGB(60, 0, 0, 0),
                  ),
                ),
                child: Row(
                  children: [
                    Expanded(
                      child: Center(
                        child: Text(
                          widget.subject.name,
                          style: const TextStyle(
                            color: mainForegroundColor,
                            fontSize: 16,
                          ),
                        ),
                      ),
                    ),
                    Text(
                      widget.subject.place,
                      style: const TextStyle(
                        color: mainForegroundColor,
                        fontSize: 16,
                      ),
                    ),
                    const SizedBox(
                      width: 14,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
