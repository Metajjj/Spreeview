﻿using AutoMapper;
using CommonLibrary.DataClasses.SeasonModel;
using Microsoft.AspNetCore.Mvc;
using SpreeviewAPI.Controllers.Interfaces;
using SpreeviewAPI.Services.Interfaces;

namespace SpreeviewAPI.Controllers.Implementations;

[ApiController]
[Route("api/[controller]")]
public class SeasonController : ControllerBase, ISeasonController
{
	private readonly IMapper _seasonMapper;
	private readonly ISeasonService _seasonService;
	public SeasonController(ISeasonService seasonService, IMapper mapper)
	{
		_seasonService = seasonService;
		_seasonMapper = mapper;
	}

	[HttpGet("{seriesId:int}/{seasonNumber:int}")]
	public async Task<ActionResult> GetSeasonByIds(int seriesId, int seasonNumber)
	{
		var response = await _seasonService.FindSeasonByIds(seriesId, seasonNumber);
		if (response == null) return NotFound("There is no season with the associated values.");
		SeasonGetDTO mappedSeasonDTO = _seasonMapper.Map<SeasonGetDTO>(response);
		return Ok(mappedSeasonDTO);
	}
}
