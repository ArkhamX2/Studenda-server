import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';
import 'package:studenda_mobile/resourses/colors.dart';
import 'package:studenda_mobile/widgets/schedule/position_values.dart';

class ScheduleItemWidget extends StatelessWidget {
  final Subject subject;
  const ScheduleItemWidget({required this.subject});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: IntrinsicHeight(
        child: Row(
          children: [
            Column(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Text(
                  dayPositionValues[subject.dayPosition],
                  textAlign: TextAlign.right,
                  style: const TextStyle(
                    color: mainForegroundColor,
                    fontSize: 16,
                  ),
                ),
              ],
            ),
            const VerticalDivider(
              width: 20,
              thickness: 1,
              indent: 5,
              endIndent: 5,
              color: Colors.grey,
            ),
            Expanded(
              child: Container(
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(5),
                  border: Border.all(
                    color: const Color.fromARGB(60, 0, 0, 0),
                  ),
                ),
                child: Row(
                  children: [
                    Expanded(
                      child: Center(
                        child: Text(
                          subject.name,
                          style: const TextStyle(
                            color: mainForegroundColor,
                            fontSize: 16,
                          ),
                        ),
                      ),
                    ),
                    Text(
                      subject.place,
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
