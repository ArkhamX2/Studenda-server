
import 'package:flutter/material.dart';
import 'package:studenda_mobile/resources/colors.dart';
import 'package:studenda_mobile/widgets/schedule/position_values.dart';

class DateCarouselWidget extends StatelessWidget {
  final List<String> dates;

  const DateCarouselWidget({
    super.key, required this.dates,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
          color: Colors.white,
        ),
        color: const Color.fromARGB(255, 211, 201, 253),
        borderRadius: const BorderRadius.all(Radius.circular(5)),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        mainAxisSize: MainAxisSize.min,
        children: [
          IconButton(
            icon: const Icon(
              Icons.chevron_left_outlined,
              color: Colors.white,
            ),
            onPressed: () {},
          ),
          Expanded(
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: dates
                  .asMap()
                  .map(
                    (key, value) => MapEntry(
                      key,
                      _DateCarouselItemWidget(
                        day: key,
                        date: value,
                      ),
                    ),
                  )
                  .values
                  .toList(),
            ),
          ),
          IconButton(
            icon: const Icon(
              Icons.chevron_right_outlined,
              color: Colors.white,
            ),
            onPressed: () {},
          ),
        ],
      ),
    );
  }
}

class _DateCarouselItemWidget extends StatelessWidget {
  final int day;
  final String date;

  const _DateCarouselItemWidget({
    required this.day,
    required this.date,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text(
          date,
          style: const TextStyle(
            color: mainForegroundColor,
            fontSize: 30,
          ),
        ),
        Text(
          weekPositionValues[day],
          style: const TextStyle(
            color: mainForegroundColor,
            fontSize: 12,
            fontWeight: FontWeight.w400,
          ),
        ),
      ],
    );
  }
}
